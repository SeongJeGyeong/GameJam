using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public struct EquippedWeapon
{
    public GlobalEnums.ItemType type;
    public int ID;
    public int attackPower;
    public float attackSpeed;
    public int durability;
    public Vector2 hitBoxSize;
}
public struct EquippedArmor
{
    public int ID;
    public int durability;
}
public struct EquipList
{
    public EquippedWeapon leftHand;
    public EquippedWeapon rightHand;
    public EquippedArmor armor;
}

public class PlayerEquipment : MonoBehaviour
{

    [SerializeField]
    SkeletonAnimation skeletonAnimation;

    EquipList equipList;

    public event Action<int> OnApplyAttackPower;
    public event Action<int> OnApplyDurability;
    // Start is called before the first frame update
    void Start()
    {
        skeletonAnimation.timeScale = 3.0f;

        equipList = new EquipList();
        equipList.leftHand.type = GlobalEnums.ItemType.EMPTY;
        equipList.leftHand.ID = -1;
        equipList.leftHand.attackSpeed = 1;
        equipList.leftHand.hitBoxSize = new Vector2(2, 1);
        equipList.rightHand.ID = -1;

        equipList.armor.ID = 0;
        ApplyEquipChange();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public EquipList GetEquipList()
    {
        return equipList;
    }

    public void SetWeaponChange(in Weapon weapon)
    {
        equipList.leftHand.type = weapon.itemType;
        equipList.leftHand.ID = weapon.GetItemNumber();
        equipList.leftHand.attackPower = weapon.GetAttackStat();
        equipList.leftHand.attackSpeed = weapon.attackSpeed;
        equipList.leftHand.hitBoxSize = (weapon.GetWeaponType() == WeaponType.Melee) ? weapon.hitBoxSize : Vector2.zero;
        OnApplyAttackPower?.Invoke(equipList.leftHand.attackPower);
        ApplyEquipChange();
    }

    public void SetArmorChange(in Equipment equipment)
    {
        equipList.armor.ID = equipment.GetItemNumber();
        equipList.armor.durability = equipment.durability;
        OnApplyDurability?.Invoke(equipment.durability);
        ApplyEquipChange();
    }

    public void ApplyEquipChange()
    {
        var skeleton = skeletonAnimation.skeleton;
        var skeletonData = skeleton.Data;

        var NewCustomSkin = new Skin("CustomCharacter");

        NewCustomSkin.AddSkin(skeletonData.FindSkin("HAIR 0"));
        NewCustomSkin.AddSkin(skeletonData.FindSkin("EYES 0"));

        if(equipList.leftHand.ID != -1)
        {
            NewCustomSkin.AddSkin(skeletonData.FindSkin(equipList.leftHand.type.ToString() + " " + equipList.leftHand.ID.ToString()));
        }

        if(equipList.rightHand.ID != -1)
        {
            NewCustomSkin.AddSkin(skeletonData.FindSkin(equipList.rightHand.type.ToString() + " " + equipList.rightHand.ID.ToString()));
        }

        if (equipList.armor.ID == 0) NewCustomSkin.AddSkin(skeletonData.FindSkin("EMPTY"));
        else NewCustomSkin.AddSkin(skeletonData.FindSkin("HELMET " + equipList.armor.ID.ToString()));

        NewCustomSkin.AddSkin(skeletonData.FindSkin("ARMOR " + equipList.armor.ID.ToString()));
        NewCustomSkin.AddSkin(skeletonData.FindSkin("SHOULDER " + equipList.armor.ID.ToString()));
        NewCustomSkin.AddSkin(skeletonData.FindSkin("ARM " + equipList.armor.ID.ToString()));
        NewCustomSkin.AddSkin(skeletonData.FindSkin("FEET " + equipList.armor.ID.ToString()));

        skeleton.SetSkin(NewCustomSkin);
        skeleton.SetSlotsToSetupPose();
    }

    public bool IsEquipped(GlobalEnums.ItemType type)
    {
        if((type == GlobalEnums.ItemType.MELEE || type == GlobalEnums.ItemType.STAFF)&& equipList.leftHand.ID == -1)
        {
            return false;
        }
        else if(type == GlobalEnums.ItemType.ARMOR && equipList.armor.ID == -1)
        {
            return false;
        }

        return true;
    }
    public EquippedWeapon GetEquippedWeapon()
    {        
        return equipList.leftHand;
    }

    public EquippedArmor GetEquippedArmor()
    {
        return equipList.armor;
    }

    public void DecreaseDurability()
    {

    }
}
