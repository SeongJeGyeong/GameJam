using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    PlayerEquipment PlayerEquipment;

    public event Action<int> OnAttack;

    void Update()
    {
        
    }

    public void Attack()
    {
        EquippedWeapon weapon = PlayerEquipment.GetEquipList().leftHand;
        OnAttack?.Invoke((int)weapon.type);
    }

    public void OnDamaging()
    {
        float flipDir = transform.localScale.x;
        EquippedWeapon weapon = PlayerEquipment.GetEquipList().leftHand;
        Vector2 HitBoxPosition = new Vector2(transform.position.x + flipDir * 2, transform.position.y + 2);
        Vector2 HitBoxSize = weapon.hitBoxSize;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(HitBoxPosition, HitBoxSize, 0f);
        foreach(Collider2D collider in colliders)
        {
            if(collider.tag == "Monster")
            {
                Monster monster = collider.GetComponent<Monster>();
                if(monster != null) monster.TakeDamage(weapon.attackPower);
            }
        }
    }



    //private void OnDrawGizmos()
    //{
    //    float flipDir = transform.localScale.x;
    //    EquippedWeapon weapon = PlayerEquipment.GetEquipList().leftHand;
    //    Vector2 HitBoxPosition = new Vector2(transform.position.x + flipDir * 2, transform.position.y + 2);
    //    Vector2 HitBoxSize = weapon.hitBoxSize;

    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(HitBoxPosition, HitBoxSize);
    //}
}
