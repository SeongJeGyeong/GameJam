using UnityEngine;

public class Slime : MonsterBase
{
    [SerializeField] private Transform atkPoint;
    [SerializeField] private float atkRange = 3f;
    [SerializeField] private int atkDamage = 1;
    [SerializeField] private LayerMask playerLayer;

    /// <summary>
    /// 플레이어를 감지했을 때 실행되는 행동 (공격)
    /// </summary>
    protected override void OnPlayerDetected()
    {
        Collider2D player = Physics2D.OverlapCircle(atkPoint.position, atkRange, playerLayer);

        if (player != null)
        {
            // player.GetComponent<Player>()?.TakeDamage(atkDamage); // 직통 공격
            Debug.Log("슬라임이 플레이어를 공격!");
        }

        // 인터페이스 방식으로 바꾸고 싶으면 이렇게
        /*
        if (player != null && player.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(atkDamage);
        }
        */
    }

    private void OnDrawGizmosSelected()
    {
        if (atkPoint != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(atkPoint.position, atkRange);
        }
    }
}
