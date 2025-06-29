using System.Collections;
using UnityEngine;

/// <summary>
/// 2D Spine 애니메이션 캐릭터 기준 카메라 따라가기 스크립트
/// </summary>
public class TestFollowCamera : MonoBehaviour
{
    [Header("타겟 (보통 플레이어)")]
    public Transform target;

    [Header("따라가는 속도")]
    public float smoothTime = 0.25f;

    [Header("카메라 위치 오프셋 (XY만 사용)")]
    public Vector2 offset = new Vector2(0f, 0f);

    [Header("카메라 이동 제한 (선택 사항)")]
    public bool useBounds = false;
    public Vector2 minPosition;
    public Vector2 maxPosition;

    private Vector3 velocity = Vector3.zero;
    private float fixedZ; // Z축 고정 값

    void Start()
    {
        // 현재 카메라 Z값을 고정값으로 저장
        fixedZ = transform.position.z;
    }

    void LateUpdate()
    {
        if (target == null)
            return;

        // 타겟 위치 + 오프셋 계산 (Z축은 고정)
        Vector3 targetPosition = new Vector3(
            target.position.x + offset.x,
            target.position.y + offset.y,
            fixedZ // Z축을 계속 고정
        );

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
