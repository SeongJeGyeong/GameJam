using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> MeleePrefabs;
    [SerializeField]
    private List<GameObject> StaffPrefabs;
    [SerializeField]
    private List<GameObject> EquipmentPrefabs;
    [SerializeField]
    private List<GameObject> ConsumptionPrefabs;

    public void SpawnItem(Vector3 pos, GlobalEnums.ItemType type, int TypeNum, int durability)
    {
        int Idx = 0;
        // TypeNum : Use Item.GetItemNumber.
        switch (type)
        {
            case GlobalEnums.ItemType.MELEE:
                switch (TypeNum)
                {
                    case 0: Idx = 0; break;
                    case 4: Idx = 1; break;
                    case 10: Idx = 2; break;
                    case 26: Idx = 3; break;
                    case 43: Idx = 4; break;
                }
                Weapon melee = Instantiate(MeleePrefabs[Idx], pos, Quaternion.identity).GetComponent<Weapon>();
                melee.durability = durability;
                break;

            case GlobalEnums.ItemType.STAFF:

                switch (TypeNum)
                {
                    case 0: Idx = 0; break;
                    case 9: Idx = 1; break;
                    case 21: Idx = 2; break;
                }
                Weapon staff = Instantiate(StaffPrefabs[Idx], pos, Quaternion.identity).GetComponent<Weapon>();
                staff.durability = durability;
                break;

            case GlobalEnums.ItemType.ARMOR:
                switch (TypeNum)
                {
                    case 0: Idx = 0; break;
                    case 1: Idx = 1; break;
                    case 5: Idx = 2; break;
                    case 6: Idx = 3; break;
                    case 30: Idx = 4; break;
                }
                Equipment armor = Instantiate(EquipmentPrefabs[Idx], pos, Quaternion.identity).GetComponent<Equipment>();
                armor.durability = durability;
                break;
        }

    }
}

//public enum SpawnType
//{
//    Melee,
//    Staff,
//    Equipment
//}