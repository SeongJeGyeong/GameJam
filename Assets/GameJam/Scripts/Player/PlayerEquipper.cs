using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class PlayerEquipper : MonoBehaviour
{
    [SerializeField]
    ItemSpawner spawner;

    List<GameObject> overlappedList;
    // Start is called before the first frame update
    void Start()
    {
        overlappedList = new List<GameObject>();
    }

    public void Equip()
    {
        if (overlappedList.Count() < 1) return;

        PlayerEquipment equipment = GetComponent<PlayerEquipment>();
        Item item = overlappedList[overlappedList.Count()-1].GetComponent<Item>();
        if (item is Weapon)
        {
            Weapon newWeapon = (Weapon)item;
            EquippedWeapon itemInfo = equipment.GetEquippedWeapon();
            if (itemInfo.ID != -1)
            {
                Vector3 spawnPosition = transform.position;
                spawnPosition.y += 2;
                spawner.SpawnItem(spawnPosition, itemInfo.type, itemInfo.ID, itemInfo.durability);
            }
            equipment.SetWeaponChange(in newWeapon);
        }
        else if(item is Equipment)
        {
            Equipment newArmor = (Equipment)item;
            EquippedArmor itemInfo = equipment.GetEquippedArmor();
            if(itemInfo.ID != 0)
            {
                Vector3 spawnPosition = transform.position;
                spawnPosition.y += 2;
                spawner.SpawnItem(spawnPosition, GlobalEnums.ItemType.ARMOR, itemInfo.ID, itemInfo.durability);
            }
            equipment.SetArmorChange(in newArmor);
        }

        Destroy(overlappedList[overlappedList.Count()-1]);
    }

    public void AddOverlappedItem(GameObject item)
    {
        overlappedList.Add(item);
    }

    public void RemoveOverlappedItem(GameObject item)
    {
        overlappedList.Remove(item);
    }

    public int GetItemListSize()
    {
        return overlappedList.Count;
    }
}
