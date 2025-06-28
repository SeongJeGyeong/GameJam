using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public struct EquipList
{
    //EquipList(string _leftHand, string _rightHand, string _armor)
    //{
    //    leftHand = _leftHand;
    //    rightHand = _rightHand;
    //    armor = _armor;
    //}

    public string leftHand;
    public string rightHand;
    public string armor;
}


public class PlayerEquipper : MonoBehaviour
{
    [SerializeField]
    SkeletonAnimation skeletonAnimation;

    EquipList equipList;
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
        if (equipList.leftHand != "" && equipList.leftHand != "EMPTY")
        {
            Vector3 spawnPosition = transform.position;
            spawnPosition.y += 2;
            //spawner.spawnItem(equipList.leftHand, spawnPosition);
        }

        //sword weapon = s.GetComponent<sword>();
        //EquippedId_leftHand = weapon.ItemID;
        //equipSystem.SetWeaponChange(weapon.weaponType, weapon.ID);
        Destroy(triggeredItem);
    }

    public void ApplyEquipChange()
    {
        var skeleton = skeletonAnimation.Skeleton;
        var skeletonData = skeleton.Data;

        var NewCustomSkin = new Skin("CustomCharacter");

        NewCustomSkin.AddSkin(skeletonData.FindSkin("HAIR 0"));
        NewCustomSkin.AddSkin(skeletonData.FindSkin("EYES 0"));

        NewCustomSkin.AddSkin(skeletonData.FindSkin(equipList.leftHand));
        NewCustomSkin.AddSkin(skeletonData.FindSkin(equipList.rightHand));

        NewCustomSkin.AddSkin(skeletonData.FindSkin("ARMOR " + equipList.armor));
        NewCustomSkin.AddSkin(skeletonData.FindSkin("SHOULDER " + equipList.armor));
        NewCustomSkin.AddSkin(skeletonData.FindSkin("HELMET " + equipList.armor));
        NewCustomSkin.AddSkin(skeletonData.FindSkin("ARM " + equipList.armor));
        NewCustomSkin.AddSkin(skeletonData.FindSkin("FEET " + equipList.armor));
        //if (Helmet == 0)
        //{
        //    NewCustomSkin.AddSkin(skeletonData.FindSkin("EMPTY"));

        //}
        //else
        //{

        //}

        skeleton.SetSkin(NewCustomSkin);
        skeleton.SetSlotsToSetupPose();
    }

    public void SetWeaponChange(GlobalEnums.EquipType Type, int Id)
    {
        //switch (Type)
        //{
        //    case GlobalEnums.EquipType.MELEE:
        //        {
        //            leftHand = "MELEE " + Id.ToString();
        //        }
        //        break;
        //    case GlobalEnums.EquipType.STAFF:
        //        {
        //            leftHand = "STAFF " + Id.ToString();
        //        }
        //        break;
        //    case GlobalEnums.EquipType.DUELISTOFFHAND:
        //        {
        //            rightHand = "OFFHAND " + Id.ToString();
        //        }
        //        break;
        //    case GlobalEnums.EquipType.ARMOR:
        //        {
        //            Armor = Id;
        //        }
        //        break;
        //    case GlobalEnums.EquipType.HELMET:
        //        {
        //            Helmet = Id;
        //        }
        //        break;
        //    case GlobalEnums.EquipType.ARM:
        //        {
        //            Arm = Id;
        //        }
        //        break;
        //    case GlobalEnums.EquipType.FEET:
        //        {
        //            Feet = Id;
        //        }
        //        break;

        //}

        ApplyEquipChange();
        Debug.Log("장비 변경");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Item")
            triggeredItem = collision.gameObject;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Item")
            triggeredItem = null;
    }
}
