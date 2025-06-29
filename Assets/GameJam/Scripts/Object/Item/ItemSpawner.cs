using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> MeleePrefabs;
    [SerializeField]
    public List<GameObject> StaffPrefabs;
    [SerializeField]
    public List<GameObject> EquipmentPrefabs;
    [SerializeField]
    private List<GameObject> ConsumptionPrefabs;
    [SerializeField]
    private List<GameObject> BulletPrefabs;
    [SerializeField]
    private List<GameObject> EffectPrefabs;

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

    public void SpawnBullet(Vector3 pos, int idx, float fireDir, int damage)
    {
        switch (idx)
        {
            case 0: idx = 0; break;
            case 9: idx = 1; break;
            case 21: idx = 2; break;
        }
        Bullet bullet = Instantiate(BulletPrefabs[idx], pos, Quaternion.identity).GetComponent<Bullet>();
        bullet.SetBulletId(idx);
        bullet.SetFireDir(fireDir);
        bullet.SetBulletDamage(damage);
        bullet.OnHit += SpawnEffect;
    }

    public void SpawnEffect(Vector3 pos, int idx)
    {
        Instantiate(EffectPrefabs[idx], pos, Quaternion.identity);
    }
}

//public enum SpawnType
//{
//    Melee,
//    Staff,
//    Equipment
//}