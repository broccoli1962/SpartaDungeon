using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public enum EItemType
{
    Equipment,
    Usable,
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
    [SerializeReference] public List<ItemEffect> usables;

    [Header("Equip")]
    public GameObject equipPrefab;
}
