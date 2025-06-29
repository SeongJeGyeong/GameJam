using UnityEngine;

public class Slime : MonsterBase
{
    [SerializeField] private float chaseSpeed;
    [SerializeField] private int atkDamage = 1;
    [SerializeField] private LayerMask playerLayer;

    /// <summary>
    /// 플레이어를 감지했을 때 실행되는 행동 (공격)
    /// </summary>
    protected override void OnPlayerDetected()
    {
        // 플레이어가 감지됐을 때 몬스터가 할 행동

        if (playerTransform == null) return;

        // 추적 속도 적용
        mover?.SetMoveSpeed(chaseSpeed);

        // 추적 이동
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        Debug.Log(direction.x);
        mover?.MoveTo(direction);

    }
}
