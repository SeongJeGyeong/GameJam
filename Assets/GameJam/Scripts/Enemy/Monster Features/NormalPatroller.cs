using UnityEngine;

/// <summary>
/// 단순 패트롤/점프 로직 담당 (몬스터 전용)
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class NormalPatroller : MonoBehaviour, IMoveable
{
    [Header("이동 체크")]
    [SerializeField] private Transform upChecker;
    [SerializeField] private Transform downChecker;

    [Header("이동 설정")]
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float jumpPower = 25f;
    private float defaultMoveSpeed;

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
        defaultMoveSpeed = moveSpeed; // 초기화 시 기본값 저장
    }


    /// <summary>
    /// 기본 패트롤 이동 (좌우 반복)
    /// </summary>
    public void Move()
    {
        if (rb == null || upChecker == null || downChecker == null)
            return;

        rb.velocity = new Vector2(-tf.localScale.x * moveSpeed, rb.velocity.y);

        bool isNoGroundAhead = !Physics2D.OverlapCircle(upChecker.position, groundCheckRadius, layerMask);
        bool isGroundBelow = Physics2D.OverlapCircle(downChecker.position, groundCheckRadius, layerMask);
        bool isFrontBlocked = Physics2D.Raycast(tf.position, -tf.localScale.x * tf.right, wallCheckDistance, layerMask);

        if (isNoGroundAhead && isGroundBelow && !isFrontBlocked)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower); // 점프
        }
        else if (isFrontBlocked)
        {
            Flip();
        }

        //if(isNoGroundAhead)
        //{
        //}

    }

    /// <summary>
    /// 방향 지정 이동 (플레이어 추적용)
    /// </summary>
    public void MoveTo(Vector2 direction)
    {
        if (rb == null)

            return;

        float moveX = direction.normalized.x;

        // 렌더링이 Flip된 상태를 기준으로 이동 방향 반전
        // localScale.x < 0 이면 오른쪽을 보고 있으므로 → 오른쪽(+x)로 이동해야 함
        // localScale.x > 0 이면 왼쪽을 보고 있으므로 → 왼쪽(-x)로 이동해야 함
        //float actualMoveDir = tf.localScale.x < 0 ? 1f : -1f;

        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
        if(rb.velocity.x < 0)
        {
            tf.localScale = new Vector3(1, tf.localScale.y);
        }
        else
        {
            tf.localScale = new Vector3(-1, tf.localScale.y);
        }
    }

    /// <summary>
    /// 좌우 뒤집기
    /// </summary>
    public void Flip()
    {
        isFlipped = !isFlipped;

        Vector3 scale = tf.localScale;
        scale.x = isFlipped ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
        tf.localScale = scale;

        rb.velocity = Vector2.zero;
    }

    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }
    public void ResetMoveSpeed()
    {
        moveSpeed = defaultMoveSpeed;
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
