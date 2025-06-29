using UnityEngine;

/// <summary>
/// ��� ������ �⺻ �ൿ (�̵� / Ž�� / ���� Ʈ����)
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

        // ���� �������� �⺻ �̵�
        if (!playerDetectedOnce)
        {
            mover?.Move();

            if (isDetectedNow)
            {
                playerTransform = detector.GetPlayerTransform(); // �÷��̾� ��ġ ����
                playerDetectedOnce = true;
                isTracking = true;
            }
        }

        // �� �� �����Ǹ� ���� ����
        if (playerDetectedOnce)
        {
            OnPlayerDetected();

            // ���� �߿� ���� �ȵǰų�, ���� ������ ���� Move() ��������.
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
    /// �÷��̾ Ž���Ǿ��� �� ȣ���
    /// </summary>
    protected abstract void OnPlayerDetected();

    /// <summary>
    /// �÷��̾ ������ �� ȣ���
    /// </summary>
    protected virtual void OnLostPlayer()
    {
        mover?.ResetMoveSpeed(); // �⺻ �ӵ��� �ǵ���
    }
}
