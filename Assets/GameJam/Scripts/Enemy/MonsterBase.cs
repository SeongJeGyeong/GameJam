using UnityEngine;

/// <summary>
/// ��� ������ �⺻ �ൿ (�̵� / Ž�� / ���� Ʈ����)
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

        // �� �� �����Ǹ� ������ ������ ����
        if (playerDetectedOnce)
        {
            mover?.Move();
        }

    }
        /// <summary>
        /// �÷��̾ Ž���Ǿ��� �� ȣ���
        /// </summary>
    protected abstract void OnPlayerDetected();
}
