using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class PlayerEquipper : MonoBehaviour
{
    [SerializeField]
    SkeletonAnimation skeletonAnimation;
    [SerializeField]
    TextMeshProUGUI text;
    [SerializeField]
    PlayerEquipment playerEquipment;
    [SerializeField]
    ItemSpawner spawner;

    GameObject triggeredItem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Equip()
    {
        if (triggeredItem == null) return;

        Item item = triggeredItem.GetComponent<Item>();
        if(item is Weapon)
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

        Destroy(triggeredItem);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Item")
        {
            text.text = "(E) Equip";
            triggeredItem = collision.gameObject;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Item")
        {
            text.text = "";
            triggeredItem = null;
        }

    }
}
