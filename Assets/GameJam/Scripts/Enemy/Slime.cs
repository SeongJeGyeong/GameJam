using UnityEngine;

public class Slime : MonsterBase
{
    [SerializeField] private int atkDamage = 1;
    [SerializeField] private LayerMask playerLayer;

    /// <summary>
    /// �÷��̾ �������� �� ����Ǵ� �ൿ (����)
    /// </summary>
    protected override void OnPlayerDetected()
    {
        // �÷��̾ �������� �� ���Ͱ� �� �ൿ



    }
}
