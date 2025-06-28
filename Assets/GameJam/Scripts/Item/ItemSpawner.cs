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
       //GameObject item = Instantiate();
    }
}

public enum SpawnType
{
    Melee,
    Staff,
    Equipment
}

public enum MeleeType
{
    // Melee 타입 번호
}

public enum StaffType
{
    // Staff 타입 번호
}

public enum EquipmentType
{
    // 장비 타입 번호 (세트)
}
