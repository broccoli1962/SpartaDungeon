using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbObject : DoSomething
{
    Rigidbody _rigid;
    public override void OnEnter(Player player)
    {
        player.controller.OnClimb = true;
    }

    public override void OnExit(Player player)
    {
        player.controller.OnClimb = false;
        if(TryGetComponent<Rigidbody>(out _rigid))
        {
            _rigid.useGravity = true;
        }
    }
}
