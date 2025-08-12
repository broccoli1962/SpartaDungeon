using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUsable
{
    public float Value { get; set; }
    public float Duration { get; set; }
    public void Apply(Player player);
}
