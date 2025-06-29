using UnityEngine;

/// <summary>
/// ��� ������ �⺻ �ൿ (�̵� / Ž�� / ���� Ʈ����)
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public abstract class MonsterBase : MonoBehaviour
{
    protected IMover mover;
    protected IPlayerDetector detector;

    protected virtual void Awake()
    {
        mover = GetComponent<IMover>();
        detector = GetComponent<IPlayerDetector>();
    }

    protected virtual void Update()
    {
        if (detector != null && detector.IsPlayerDetected())
        {
            OnPlayerDetected();
        }
        else
        {
            mover?.Move();
        }
    }

    /// <summary>
    /// �÷��̾ Ž���Ǿ��� �� ȣ���
    /// </summary>
    protected abstract void OnPlayerDetected();
}
