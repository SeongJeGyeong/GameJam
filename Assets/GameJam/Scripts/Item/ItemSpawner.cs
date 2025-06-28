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

    public void SpawnItem(Vector3 pos, SpawnType type, int TypeNum)
    {
        // TypeNum : Use Item.GetItemNumber.

        switch(type)
        {
            case SpawnType.Melee:
                switch (TypeNum)
                {
                    case 0:
                        Instantiate(MeleePrefabs[0], pos, Quaternion.identity);
                        break;
                    case 4:
                        Instantiate(MeleePrefabs[1], pos, Quaternion.identity);
                        break;

                    case 10:
                        Instantiate(MeleePrefabs[2], pos, Quaternion.identity);
                        break;

                    case 26:
                        Instantiate(MeleePrefabs[3], pos, Quaternion.identity);
                        break;

                    case 43:
                        Instantiate(MeleePrefabs[4], pos, Quaternion.identity);
                        break;
                }
                break;

            case SpawnType.Staff:

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

            case SpawnType.Equipment:
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

public enum SpawnType
{
    Melee,
    Staff,
    Equipment
}