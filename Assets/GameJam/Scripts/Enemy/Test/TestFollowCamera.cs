using System.Collections;
using UnityEngine;

/// <summary>
/// 2D Spine �ִϸ��̼� ĳ���� ���� ī�޶� ���󰡱� ��ũ��Ʈ
/// </summary>
public class TestFollowCamera : MonoBehaviour
{
    [Header("Ÿ�� (���� �÷��̾�)")]
    public Transform target;

    [Header("���󰡴� �ӵ�")]
    public float smoothTime = 0.25f;

    [Header("ī�޶� ��ġ ������ (XY�� ���)")]
    public Vector2 offset = new Vector2(0f, 0f);

    [Header("ī�޶� �̵� ���� (���� ����)")]
    public bool useBounds = false;
    public Vector2 minPosition;
    public Vector2 maxPosition;

    private Vector3 velocity = Vector3.zero;
    private float fixedZ; // Z�� ���� ��

    void Start()
    {
        // ���� ī�޶� Z���� ���������� ����
        fixedZ = transform.position.z;
    }

    void LateUpdate()
    {
        if (target == null)
            return;

        // Ÿ�� ��ġ + ������ ��� (Z���� ����)
        Vector3 targetPosition = new Vector3(
            target.position.x + offset.x,
            target.position.y + offset.y,
            fixedZ // Z���� ��� ����
        );

        // �ε巯�� �̵�
        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        // �̵� ������ �����Ǿ� �ִٸ� ����
        if (useBounds)
        {
            smoothPosition.x = Mathf.Clamp(smoothPosition.x, minPosition.x, maxPosition.x);
            smoothPosition.y = Mathf.Clamp(smoothPosition.y, minPosition.y, maxPosition.y);
        }

        transform.position = smoothPosition;
    }
}
