using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFollowCamera : MonoBehaviour
{
    [Header("타겟 (보통 플레이어)")]
    public Transform target;

    [Header("따라가는 속도")]
    public float smoothTime = 0.25f;

    [Header("카메라 위치 오프셋")]
    public Vector3 offset = new Vector3(0f, 0f, -10f);

    [Header("카메라 이동 제한 (선택 사항)")]
    public bool useBounds = false;
    public Vector2 minPosition;
    public Vector2 maxPosition;

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null)
            return;

        // 타겟 위치 + 오프셋 계산
        Vector3 targetPosition = target.position + offset;

        // 부드러운 이동
        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        // 이동 제한이 설정되어 있다면 적용
        if (useBounds)
        {
            smoothPosition.x = Mathf.Clamp(smoothPosition.x, minPosition.x, maxPosition.x);
            smoothPosition.y = Mathf.Clamp(smoothPosition.y, minPosition.y, maxPosition.y);
        }

        transform.position = smoothPosition;
    }
}
