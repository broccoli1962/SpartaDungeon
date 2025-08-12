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
            Debug.Log($"ȸ�� {value}");
            yield break;
        }
        else
        {
            float time = 0f;
            while (time < duration)
            {
                player.condition.Heal(value);
                Debug.Log($"����ȸ�� {value} �����ð� {duration - time}");
                yield return new WaitForSeconds(tick);
                time += tick;
            }
        }
    }
}