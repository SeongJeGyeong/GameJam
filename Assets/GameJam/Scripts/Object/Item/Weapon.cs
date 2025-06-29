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

    // attack ������ �� ���.
    public int GetAttackStat() {  return attack; }

    // weaponType ������ �� ���. int ������ ��ȯ.
    public WeaponType GetWeaponType() { return weaponType; }
}

public enum WeaponType
{
    Melee,
    Staff
}