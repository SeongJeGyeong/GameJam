using UnityEngine;

public class Slime : MonsterBase
{
    //[SerializeField] private float chaseSpeed;
    //[SerializeField] private int atkDamage = 1;
    //[SerializeField] private LayerMask playerLayer;

    /// <summary>
    /// �÷��̾ �������� �� ����Ǵ� �ൿ (����)
    /// </summary>
    protected override void OnPlayerDetected()
    {
        // �÷��̾ �������� �� ���Ͱ� �� �ൿ

        //if (playerTransform == null) return;

        //// ���� �ӵ� ����
        //mover?.SetMoveSpeed(chaseSpeed);

        //// ���� �̵�
        //Vector2 direction = (playerTransform.position - transform.position).normalized;
        //mover?.MoveTo(direction);

    }
}
