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
            // �¿� �̵�
            rb.velocity = new Vector2(-transform.localScale.x * moveSpeed, rb.velocity.y);

            // ���� ����
            // wallCheck[0] �� ���� �̵����� ��ȯ
            // wallCheck[1] �� �ö� �� �ִ� �� Ȯ�� �� ����

            bool isNoGroundFront = !Physics2D.OverlapCircle(wallCheck[0].position, 0.01f, layerMask); 
            bool isGroundBack = Physics2D.OverlapCircle(wallCheck[1].position, 0.01f, layerMask);
            bool isNotBlocked = !Physics2D.Raycast(transform.position, -transform.localScale.x * transform.right, 1f, layerMask);

            if (isNoGroundFront && isGroundBack && isNotBlocked)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            }
            // ���� ������ ���� ��ȯ
            else if (isGroundBack)
            {
                Debug.Log("������ - ���� �浹");
                MonsterFlip();
            }
        }
    }

    protected new void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        // �� ���̾�� �浹 �� ���� ��ȯ

        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log("������ - �÷��̾�� �浹");
            MonsterFlip();
        }
    }



}
