using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : Item
{
    public Equipment()
    {
        durability = 0;
        base.itemType = GlobalEnums.ItemType.ARMOR;
        base.SetItemNumber(0);
    }

    public int durability;
}
