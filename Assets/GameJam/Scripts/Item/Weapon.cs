using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    [SerializeField]
    WeaponType weaponType;
    [SerializeField]
    private int attack;

    public float attackSpeed;
    public Vector2 hitBoxSize;
    public int durability;

    // attack 가져올 때 사용.
    public int GetAttackStat() {  return attack; }

    // weaponType 가져올 때 사용. int 형으로 반환.
    public int GetWeaponType() { return (int)weaponType; }
}

public enum WeaponType
{
    Melee,
    Staff
}