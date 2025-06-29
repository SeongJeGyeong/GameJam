using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    Rigidbody2D rb;
    float moveDir;

    [Header("Move Settings")]
    public float moveSpeed = 5f;
    public float jumpPower = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveDir = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDir * moveSpeed, rb.velocity.y);
    }
}
