using UnityEngine;

/// <summary>
/// 모든 몬스터의 기본 행동 (이동 / 탐지 / 공격 트리거)
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public abstract class MonsterBase : MonoBehaviour
{
    protected bool isTracking = false;
    protected IMoveable mover;
    protected IPlayerDetectable detector;
    protected bool playerDetectedOnce = false;
    protected Transform playerTransform;

    protected virtual void Awake()
    {
        mover = GetComponent<IMoveable>();
        detector = GetComponent<IPlayerDetectable>();
    }

    protected virtual void Update()
    {
        bool isDetectedNow = detector != null && detector.IsPlayerDetected();

        // 감지 이전에는 기본 이동
        if (!playerDetectedOnce)
        {
            mover?.Move();

            if (isDetectedNow)
            {
                playerTransform = detector.GetPlayerTransform(); // 플레이어 위치 추출
                playerDetectedOnce = true;
                isTracking = true;
            }
        }

        // 한 번 감지되면 추적 시작
        if (playerDetectedOnce)
        {
            OnPlayerDetected();

            // 추적 중에 감지 안되거나, 벽이 있으면 원래 Move() 로직으로.
            if (!isDetectedNow)
            {
                OnLostPlayer();
                playerDetectedOnce = false;
                isTracking = false;
            }
            //else if(mover.IsBlocked())
            //{
            //    OnLostPlayer();
            //    playerDetectedOnce = false;
            //    isTracking = false;
            //}
        }
    }

    /// <summary>
    /// 플레이어가 탐지되었을 때 호출됨
    /// </summary>
    protected abstract void OnPlayerDetected();

    /// <summary>
    /// 플레이어를 놓쳤을 때 호출됨
    /// </summary>
    protected virtual void OnLostPlayer()
    {
        mover?.ResetMoveSpeed(); // 기본 속도로 되돌림
    }
}
