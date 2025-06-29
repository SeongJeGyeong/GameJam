using UnityEngine;

public class Slime : MonsterBase
{
    [SerializeField] private Transform atkPoint;
    [SerializeField] private float atkRange = 3f;
    [SerializeField] private int atkDamage = 1;
    [SerializeField] private LayerMask playerLayer;

    /// <summary>
    /// �÷��̾ �������� �� ����Ǵ� �ൿ (����)
    /// </summary>
    protected override void OnPlayerDetected()
    {
        Collider2D player = Physics2D.OverlapCircle(atkPoint.position, atkRange, playerLayer);

        if (player != null)
        {
            // player.GetComponent<Player>()?.TakeDamage(atkDamage); // ���� ����
            Debug.Log("�������� �÷��̾ ����!");
        }

        // �������̽� ������� �ٲٰ� ������ �̷���
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
