using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlace : DoSomething
{
    public float JumpPlacePower = 10f;
    public override void OnEnter(Player player)
    {
        Rigidbody body = player.GetComponent<Rigidbody>();
        if(body != null)
        {
            body.AddForce(JumpPlacePower * Vector3.up, ForceMode.Impulse);
        }
    }

    public override void OnExit(Player player)
    {
        //nothing
    }
}
