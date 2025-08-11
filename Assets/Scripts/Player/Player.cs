using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public PlayerCondition condition;

    private void Awake()
    {
        PlayerManager.Instance.player = this;
        controller = GetComponent<PlayerController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DoSomething"))
        {
            Debug.Log("�ݸ��� Ʈ���� ��");
            DoSomething trigger = collision.gameObject.GetComponent<DoSomething>();
            trigger.OnEnter(this);
        }
    }
}
