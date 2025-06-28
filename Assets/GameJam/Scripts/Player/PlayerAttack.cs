using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public event Action OnAttack;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Attack()
    {
        OnAttack?.Invoke();
    }

    public void OnDamaging()
    {
        Vector2 HitBoxPosition = new Vector2(transform.position.x + 2, transform.position.y);
        Vector2 HitBoxSize = new Vector2(3, 4);
        Collider2D[] colliders = Physics2D.OverlapBoxAll(HitBoxPosition, HitBoxSize, 0f);
        OnDrawGizmos();
    }

    private void OnDrawGizmos()
    {
        Vector2 HitBoxPosition = new Vector2(transform.position.x + 2, transform.position.y);
        Vector2 HitBoxSize = new Vector2(3, 4);

        //Gizmos.color = Color.red;
        //Gizmos.DrawWireCube(HitBoxPosition, HitBoxSize);
    }
}
