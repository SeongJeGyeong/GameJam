using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float speed;
    float fireDir = 1;
    int bulletId = 0;
    int bulletDamage = 0;

    public event Action<Vector3, int> OnHit;

    void Start()
    {
        Invoke("DestroyBullet", 2);    
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.right * fireDir * speed * Time.deltaTime);
    }

    public void SetBulletId(int id)
    {
        bulletId = id;
    }
    public void SetFireDir(float dir)
    {
        fireDir = dir;
    }
    public void SetBulletDamage(int damage)
    {
        bulletDamage = damage;
    }

    void DestroyBullet()
    {
        OnHit?.Invoke(transform.position, bulletId);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            DestroyBullet();
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            // Damaged Monster
            Monster monster = collision.GetComponent<Monster>();
            if (monster != null) monster.TakeDamage(bulletDamage);

            DestroyBullet();
        }
    }
}
