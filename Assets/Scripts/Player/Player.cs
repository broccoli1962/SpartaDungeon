using System;
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
        condition = GetComponent<PlayerCondition>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DoSomething"))
        {
            Debug.Log("콜리전 트리거 온");
            DoSomething trigger = collision.gameObject.GetComponent<DoSomething>();
            trigger.OnEnter(this);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("DoSomething"))
        {
            DoSomething trigger = collision.gameObject.GetComponent<DoSomething>();
            trigger.OnExit(this);
        }
    }


    public void OnUseItem(ItemData data)
    {
        if(data.itemType == EItemType.Usable)
        {
            foreach (var effect in data.usables)
            {
                StartCoroutine(effect.Apply(this));
            }
        }
        else if(data.itemType == EItemType.Equipment)
        {
            GameObject obj = Instantiate(data.equipPrefab, transform.position + new Vector3(0, 0, 2), Quaternion.identity);
            obj.transform.SetParent(this.transform);
        }
    }
}
