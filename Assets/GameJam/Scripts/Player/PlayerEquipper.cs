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
    TextMeshProUGUI text;
    [SerializeField]
    PlayerEquipment playerEquipment;
    [SerializeField]
    ItemSpawner spawner;

    List<GameObject> overlappedList;
    // Start is called before the first frame update
    void Start()
    {
        overlappedList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
       //Collider2D[] Overlapped = Physics2D.OverlapCapsuleAll(new Vector2(0, 1.6f), new Vector2(4, 3.8f), CapsuleDirection2D.Vertical, 0);
       // foreach(Collider2D overlapObject in Overlapped)
       // {
       //     if (overlapObject.tag == "Item")
       //     {
       //         overlappedCheckList.Add(overlapObject.gameObject);
       //     }
       // }
       // foreach(GameObject item in overlappedList)
       // {
       //     if(!overlappedCheckList.Contains(item))
       //     {
       //         overlappedList.Remove(item);
       //     }
       // }
    }

    public void Equip()
    {
        if (overlappedList.Count() < 1) return;

        Item item = overlappedList[overlappedList.Count()-1].GetComponent<Item>();
        if (item is Weapon)
        {
            Weapon newWeapon = (Weapon)item;
            EquippedWeapon itemInfo = playerEquipment.GetEquippedWeapon();
            if (itemInfo.ID != -1)
            {
                Vector3 spawnPosition = transform.position;
                spawnPosition.y += 2;
                spawner.SpawnItem(spawnPosition, itemInfo.type, itemInfo.ID, itemInfo.durability);
            }
            playerEquipment.SetWeaponChange(in newWeapon);
        }
        else if(item is Equipment)
        {
            Equipment newArmor = (Equipment)item;
            EquippedArmor itemInfo = playerEquipment.GetEquippedArmor();
            if(itemInfo.ID != 0)
            {
                Vector3 spawnPosition = transform.position;
                spawnPosition.y += 2;
                spawner.SpawnItem(spawnPosition, GlobalEnums.ItemType.ARMOR, itemInfo.ID, itemInfo.durability);
            }
            playerEquipment.SetArmorChange(in newArmor);
        }

        //overlappedList.RemoveAt(overlappedList.Count()-1);
        Destroy(overlappedList[overlappedList.Count()-1]);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Item")
        {
            text.text = "(E) Equip";
            overlappedList.Add(collision.gameObject);
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Item")
        {
            overlappedList.Remove(collision.gameObject);
        }

        if(overlappedList.Count() < 1)
        {
            text.text = "";
        }
    }
}
