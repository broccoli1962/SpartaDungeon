using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed;
    public float jumpPower;
    public Vector2 currentInput;
    public LayerMask groundLayerMask;

    [Header("Look")]
    public Transform cameraContainer;
    public float minX;
    public float maxX;
    public float camRotX;
    public float lookSensitivity;
    private Vector2 mouseDelta;

    private Rigidbody _rigidbody;

    public void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void FixedUpdate()
    {
        Move();
    }

    public void LateUpdate()
    {
        CameraLook();
    }

    void Move()
    {
        Vector3 dir = transform.forward * currentInput.y + transform.right * currentInput.x;
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y;

        _rigidbody.velocity = dir;
    }

    void CameraLook()
    {
        camRotX += mouseDelta.y * lookSensitivity;
        camRotX = Mathf.Clamp(camRotX, minX, maxX);
        cameraContainer.localEulerAngles = new Vector3(-camRotX, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed) {
            currentInput = context.ReadValue<Vector2>();
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            currentInput = Vector2.zero;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("키 입력 점프");
        
        if(context.phase == InputActionPhase.Started && IsGround())
        {
            PlayerManager.Instance.player.condition.Stamina.RemoveValue(20);
            _rigidbody.AddForce(jumpPower * Vector2.up, ForceMode.Impulse);
        }
    }

    private bool IsGround()
    {
        // 콜라이더 길이를 받아서와서 캡슐 콜라이더의 밑에서 시작할수 있게한다
        Ray ray = new Ray(transform.position, Vector2.down);

        if (Physics.Raycast(ray, 1.5f, groundLayerMask))
        {
            return true;
        }
        return false;
    }
}
