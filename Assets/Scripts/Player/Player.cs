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
}
