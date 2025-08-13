using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : DoSomething
{
    public float movementSpeed;
    public float moveX;
    public float moveY;

    public Vector3 beforePlace;
    public Vector3 afterPlace;

    private void Awake()
    {
        beforePlace = transform.position;
        afterPlace = new Vector3(beforePlace.x + moveX, beforePlace.y + moveY, beforePlace.z);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, afterPlace, movementSpeed * Time.deltaTime);
    }

    public override void OnEnter(Player player)
    {
        player.transform.SetParent(this.transform);
    }

    public override void OnExit(Player player)
    {
        player.transform.SetParent(null);
    }
}
