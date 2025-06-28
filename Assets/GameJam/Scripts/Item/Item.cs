using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum ItemType
//{
//    Weapon,
//    Equipment,
//    Consumption
//}

public class Item : MonoBehaviour
{

    public GlobalEnums.ItemType itemType;
    [SerializeField]
    private int itemNumber; // 해당 아이템 번호. 아이템 부위 + itemNuber.Tostring 으로 사용할 거 같아서

    public int GetItemNumber() { return itemNumber; }
}
