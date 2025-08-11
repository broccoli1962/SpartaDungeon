using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    [Header("InteractionSet")]
    public float interactRange;
    public float interactRate = 0.05f;
    public float interactCheckTime;
    public LayerMask layerMask;

    public GameObject curInteractObject;
    private IInteractable curInteractable;

    public TextMeshProUGUI promptText;
    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        if (Time.time - interactCheckTime > interactRate) {
            interactCheckTime = Time.time;
            //1-0 > 0.05f; 0.05 간격으로 체크
            Ray ray = camera.ScreenPointToRay(new Vector2(Screen.width/2f, Screen.height/2f+100));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactRange, layerMask))
            {
                if (hit.collider.gameObject != curInteractObject)
                {
                    curInteractObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                    SetPromptText();
                }
            }
            else
            {
                curInteractObject = null;
                curInteractable = null;
                promptText.gameObject.SetActive(false);
            }
        }
    }

    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = curInteractable.GetInteractPrompt();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && curInteractable != null) {
            curInteractable.OnInteract();
            curInteractObject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }
}
