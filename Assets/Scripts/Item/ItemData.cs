using OpenCover.Framework.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EItemType
{
    Equipment,
    Usable,
}

public enum EUsableType
{
    Health,
    Hunger,
    Stamina
}
public enum EUsableEffectType
{
    Speed,
    Regenation,
}

[Serializable]
public class ItemDataUsableEffect
{
    public EUsableEffectType UsableEffect;
    public float value;
    public float durationValue;
}

[Serializable]
public class ItemDataUsable
{
    public EUsableType Usable;
    public float value;
}

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable/ItemData")]
public class ItemData : ScriptableObject
{
    [Header("ItemInfo")]
    public EItemType itemType;
    public string displayName;
    public string description;
    public Sprite icon;
    public GameObject dropObject;

    [Header("Stackable")]
    public bool canStack;
    public int maxStackAmount;

    [Header("Usable")]
    public ItemDataUsable[] usables;

    [Header("UsableDuration")]
    public ItemDataUsableEffect[] usableEffects;

    [Header("Equip")]
    public GameObject equipPrefab;
}
