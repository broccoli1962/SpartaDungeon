using System;
using System.Collections;
using UnityEngine;

[Serializable]
public abstract class ItemEffect
{
    public float value;
    public float tick;
    public float duration;

    public abstract IEnumerator Apply(Player player);
}
