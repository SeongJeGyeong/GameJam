using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFollowCamera : MonoBehaviour
{
    [Header("Ÿ�� (���� �÷��̾�)")]
    public Transform target;

    [Header("���󰡴� �ӵ�")]
    public float smoothTime = 0.25f;

    [Header("ī�޶� ��ġ ������")]
    public Vector3 offset = new Vector3(0f, 0f, -10f);

    [Header("ī�޶� �̵� ���� (���� ����)")]
    public bool useBounds = false;
    public Vector2 minPosition;
    public Vector2 maxPosition;

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null)
            return;

        // Ÿ�� ��ġ + ������ ���
        Vector3 targetPosition = target.position + offset;

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
