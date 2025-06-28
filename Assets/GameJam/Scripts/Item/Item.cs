using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Equipment,
    Consumption
}

public abstract class Item : MonoBehaviour
{
    [SerializeField]
    public ItemType itemType;

    public abstract void Use();
}
