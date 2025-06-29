using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBridge : MonoBehaviour
{
    [SerializeField]
    Vector3 startPos;
    [SerializeField]
    Vector3 endPos;
    [SerializeField]
    float speed;
    Vector3 desPos;


    void Start()
    {
        transform.position = startPos;
        desPos = endPos;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, desPos, Time.deltaTime * speed);
        if (Vector2.Distance(transform.position, desPos) < 0.05f)
        {
            if (desPos == endPos) desPos = startPos;
            else desPos = endPos;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            // �÷��̾ ������ ���� ��츸 (������ ���� ����)
            if (contact.normal.y < -0.5f)
            {
                // �÷��̾ �� �÷����� �ڽ����� ����
                collision.transform.SetParent(transform);
                break;
            }
        }

        //collision.gameObject.transform.parent = gameObject.transform;
        //transform.parent = collision
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        collision.gameObject.transform.parent = null;
        //transform.parent = collision
    }
}
