using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salamander : MonsterBase
{
    [SerializeField] private int atkDamage = 1;
    [SerializeField] private LayerMask playerLayer;

    /// <summary>
    /// 플레이어를 감지했을 때 실행되는 행동 (공격)
    /// </summary>
    protected override void OnPlayerDetected()
    {
        // 플레이어가 감지됐을 때 몬스터가 할 행동
        


    }
}

