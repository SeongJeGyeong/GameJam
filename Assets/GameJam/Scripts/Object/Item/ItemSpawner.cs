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
        // TypeNum : Use Item.GetItemNumber.
        switch(type)
        {
            case GlobalEnums.ItemType.MELEE:
                int idx = 0;
                switch (TypeNum)
                {
                    case 0: idx = 0; break;
                    case 4: idx = 1; break;
                    case 10: idx = 2; break;
                    case 26: idx = 3; break;
                    case 43: idx = 4; break;
                }
                Weapon weapon = Instantiate(MeleePrefabs[idx], pos, Quaternion.identity).GetComponent<Weapon>();
                weapon.durability = durability;
                break;

            case GlobalEnums.ItemType.STAFF:

                switch (TypeNum)
                {
                    case 0:
                        Instantiate(StaffPrefabs[0], pos, Quaternion.identity);
                        break;
                    case 9:
                        Instantiate(StaffPrefabs[1], pos, Quaternion.identity);
                        break;
                    case 21:
                        Instantiate(StaffPrefabs[2], pos, Quaternion.identity);
                        break;
                }
                break;

            case GlobalEnums.ItemType.ARMOR:
                switch (TypeNum)
                {
                    case 0:
                        Instantiate(EquipmentPrefabs[0], pos, Quaternion.identity);
                        break;
                    case 4:
                        Instantiate(EquipmentPrefabs[1], pos, Quaternion.identity);
                        break;

                    case 10:
                        Instantiate(EquipmentPrefabs[2], pos, Quaternion.identity);
                        break;

                    case 26:
                        Instantiate(EquipmentPrefabs[3], pos, Quaternion.identity);
                        break;

                    case 43:
                        Instantiate(EquipmentPrefabs[4], pos, Quaternion.identity);
                        break;
                }
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