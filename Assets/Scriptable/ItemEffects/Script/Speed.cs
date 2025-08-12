using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class Speed : ItemEffect
{
    public override IEnumerator Apply(Player player)
    {
        float speed = player.controller.moveSpeed;
        float time = 0f;

        player.controller.moveSpeed += value;

        yield return new WaitForSeconds(duration);

        player.controller.moveSpeed = speed;
    }
}