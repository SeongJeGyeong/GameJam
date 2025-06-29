using UnityEngine;

/// <summary>
/// 모든 몬스터의 기본 행동 (이동 / 탐지 / 공격 트리거)
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public abstract class MonsterBase : MonoBehaviour
{
    protected IMover mover;
    protected IPlayerDetector detector;
    protected bool playerDetectedOnce = false;

    protected virtual void Awake()
    {
        mover = GetComponent<IMover>();
        detector = GetComponent<IPlayerDetector>();
    }

    protected virtual void Update()
    {
        if (!playerDetectedOnce)
        {
            if (detector != null && detector.IsPlayerDetected())
            {
                playerDetectedOnce = true;
                OnPlayerDetected();
            }
        }

        // 한 번 감지되면 무조건 움직임 시작
        if (playerDetectedOnce)
        {
            mover?.Move();
        }

    }
        /// <summary>
        /// 플레이어가 탐지되었을 때 호출됨
        /// </summary>
    protected abstract void OnPlayerDetected();
}
