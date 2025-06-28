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

    void Start()
    {
        
    }

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
        Vector2 HitBoxPosition = new Vector2(transform.position.x + flipDir * 2, transform.position.y + 2);
        Vector2 HitBoxSize = new Vector2(5, 5);
        Collider2D[] colliders = Physics2D.OverlapBoxAll(HitBoxPosition, HitBoxSize, 0f);
    }

    private void OnDrawGizmos()
    {
        float flipDir = transform.localScale.x;
        Vector2 HitBoxPosition = new Vector2(transform.position.x + flipDir * 2, transform.position.y + 2);
        Vector2 HitBoxSize = new Vector2(5, 5);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(HitBoxPosition, HitBoxSize);
    }
}
