using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Heal : ItemEffect
{
    public bool regenHealth = false;
    public override IEnumerator Apply(Player player)
    {
        if (!regenHealth) {
            player.condition.Heal(value);
            Debug.Log($"회복 {value}");
            yield break;
        }
        else
        {
            float time = 0f;
            while (time < duration)
            {
                player.condition.Heal(value);
                Debug.Log($"지속회복 {value} 남은시간 {duration - time}");
                yield return new WaitForSeconds(tick);
                time += tick;
            }
        }
    }
}