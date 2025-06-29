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
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                // 플레이어가 위에서 밟은 경우만 (법선이 위를 향함)
                if (contact.normal.y < -0.5f)
                {
                    // 플레이어를 이 플랫폼의 자식으로 설정
                    collision.transform.SetParent(transform);
                    break;
                }
            }
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.transform.parent = null;
        }
    }
}
