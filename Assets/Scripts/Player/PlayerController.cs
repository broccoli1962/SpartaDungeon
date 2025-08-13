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
    public bool OnClimb = false;

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
        if (OnClimb)
        {
            Climb();
        }
        else
        {
            Move();
        }
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

    void Climb()
    {
        Vector3 climbVelocity = new Vector3(currentInput.x, currentInput.y, 0);
        _rigidbody.velocity = climbVelocity;
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
        Debug.Log("Ű �Է� ����");

        if (context.phase == InputActionPhase.Started && IsGround() || OnClimb)
        {
            if (OnClimb && !IsGround()) {
                OnClimb = false;
            }
            PlayerManager.Instance.player.condition.Stamina.RemoveValue(20);
            _rigidbody.AddForce(jumpPower * Vector2.up, ForceMode.Impulse);
        }
    }

    private bool IsGround()
    {
        // �ݶ��̴� ���̸� �޾Ƽ��ͼ� ĸ�� �ݶ��̴��� �ؿ��� �����Ҽ� �ְ��Ѵ�
        Ray ray = new Ray(transform.position, Vector2.down);

        if (Physics.Raycast(ray, 1.5f, groundLayerMask))
        {
            return true;
        }
        return false;
    }
}
