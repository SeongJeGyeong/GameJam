using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float speed;

    void Start()
    {
        Invoke("DestroyBullet", 2);    
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.right * speed * Time.deltaTime);
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            //effect
            Destroy(this);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            // Damaged Monster
        }
    }

}
