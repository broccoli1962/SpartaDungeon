using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class DoSomething : MonoBehaviour
{
    public abstract void OnEnter(Player player);
    public abstract void OnExit(Player player);
}
