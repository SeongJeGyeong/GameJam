using UnityEngine;

/// <summary>
/// 단순 패트롤/점프 로직 담당 (몬스터 전용)
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class NormalPatroller : MonoBehaviour, IMover
{
    [Header("이동 체크")]
    [SerializeField] private Transform upChecker;
    [SerializeField] private Transform downChecker;

    [Header("이동 설정")]
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float jumpPower = 25f;

    [Header("감지 거리")]
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private float wallCheckDistance = 2f;

    private Rigidbody2D rb;
    private Transform tf;
    private bool isFlipped = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        tf = transform;
    }

    public void Move()
    {
        if (rb == null || upChecker == null || downChecker == null)
            return;

        rb.velocity = new Vector2(-tf.localScale.x * moveSpeed, rb.velocity.y);

        bool noGroundAhead = !Physics2D.OverlapCircle(upChecker.position, groundCheckRadius, layerMask);
        bool groundBelow = Physics2D.OverlapCircle(downChecker.position, groundCheckRadius, layerMask);
        bool frontWall = Physics2D.Raycast(tf.position, -tf.localScale.x * tf.right, wallCheckDistance, layerMask);

        if (noGroundAhead && groundBelow && !frontWall)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower); // 점프
        }
        else if (frontWall)
        {
            Flip();
        }
    }

    public void Flip()
    {
        isFlipped = !isFlipped;

        Vector3 scale = tf.localScale;
        scale.x = isFlipped ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
        tf.localScale = scale;

        rb.velocity = Vector2.zero;

    }

    private void OnDrawGizmosSelected()
    {
        if (upChecker != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(upChecker.position, groundCheckRadius);
        }

        if (downChecker != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(downChecker.position, groundCheckRadius);
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position,
                        transform.position + (-transform.localScale.x * transform.right) * wallCheckDistance);
    }
}
