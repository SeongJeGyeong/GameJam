using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salamander : MonsterBase
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

