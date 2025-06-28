using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Monster
{
    public Transform[] wallCheck;

    private new void Awake()
    {
        base.Awake();

//        moveSpeed = 3f;
//        jumpPower = 25f;



        //        Debug.Log("Wall 포함 여부: " + (((1 << LayerMask.NameToLayer("Wall")) & layerMask.value) != 0));
        //        layerMask = LayerMask.GetMask("Wall");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHit)
        {
            rb.velocity = new Vector2(-transform.localScale.x * moveSpeed, rb.velocity.y);

            if (!Physics2D.OverlapCircle(wallCheck[0].position, 0.01f, LayerMask.GetMask("Ground")) &&
                Physics2D.OverlapCircle(wallCheck[1].position, 0.01f, LayerMask.GetMask("Ground")) &&
                 !Physics2D.Raycast(transform.position, -transform.localScale.x * transform.right, 1f, LayerMask.GetMask("Ground")))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            }
            else if (Physics2D.OverlapCircle(wallCheck[1].position, 0.01f, LayerMask.GetMask("Wall")))
            {
                //Debug.Log("몬스터 벽 충돌");
                MonsterFlip();
            }
        }

    }

    void OnDrawGizmos()
    {
        if (wallCheck != null && wallCheck.Length >= 2)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(wallCheck[0].position, 0.2f); // up
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(wallCheck[1].position, 0.2f); // down
        }
    }
    protected new void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.transform.CompareTag("Player"))
        {
            MonsterFlip();
        }
    }



}
