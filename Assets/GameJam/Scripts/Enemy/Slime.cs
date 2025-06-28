using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Monster
{
    public Transform[] wallCheck;

    protected new void Awake()
    {
        base.Awake();
        moveSpeed = 2f;
        jumpPower = 15f;
    }

    void Update()
    {
        if (!isHit)
        {
            // 좌우 이동
            rb.velocity = new Vector2(-transform.localScale.x * moveSpeed, rb.velocity.y);

            // 점프 조건
            // wallCheck[0] 는 몬스터 이동방향 전환
            // wallCheck[1] 는 올라갈 수 있는 곳 확인 및 점프

            bool isNoGroundFront = !Physics2D.OverlapCircle(wallCheck[0].position, 0.01f, layerMask); 
            bool isGroundBack = Physics2D.OverlapCircle(wallCheck[1].position, 0.01f, layerMask);
            bool isNotBlocked = !Physics2D.Raycast(transform.position, -transform.localScale.x * transform.right, 1f, layerMask);

            if (isNoGroundFront && isGroundBack && isNotBlocked)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            }
            // 벽에 닿으면 방향 전환
            else if (isGroundBack)
            {
                Debug.Log("슬라임 - 벽에 충돌");
                MonsterFlip();
            }
        }
    }

    protected new void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        // 벽 레이어와 충돌 시 방향 전환

        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log("슬라임 - 플레이어와 충돌");
            MonsterFlip();
        }
    }



}
