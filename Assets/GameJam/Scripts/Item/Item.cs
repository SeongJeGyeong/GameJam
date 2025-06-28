using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Equipment,
    Consumption
}

public class Item : MonoBehaviour
{
    [SerializeField]
    public ItemType itemType;
    [SerializeField]
    private int itemNumber; // �ش� ������ ��ȣ. ������ ���� + itemNuber.Tostring ���� ����� �� ���Ƽ�

    public int GetItemNumber() { return itemNumber; }
}
