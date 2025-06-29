using UnityEngine;

public class RaycastPlayerDetector : MonoBehaviour, IPlayerDetector
{
    [SerializeField] private float detectionDistance = 7f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform rayOrigin;
    [SerializeField] private Vector2 direction = Vector2.left;

    private void Reset()
    {
        rayOrigin = transform;
    }

    public bool IsPlayerDetected()
    {
        Vector2 dirNormalized = direction.normalized;

        // 왼쪽 방향 감지
        RaycastHit2D hit1 = Physics2D.Raycast(rayOrigin.position, -dirNormalized, detectionDistance, playerLayer);
        // 오른쪽 방향 감지
        RaycastHit2D hit2 = Physics2D.Raycast(rayOrigin.position, dirNormalized, detectionDistance, playerLayer);

        return hit1.collider != null || hit2.collider != null;
    }

    private void OnDrawGizmosSelected()
    {
        if (rayOrigin == null) return;

        Vector2 dirNormalized = direction.normalized;
        Gizmos.color = Color.cyan;

        // 오른쪽 방향
        Gizmos.DrawLine(rayOrigin.position, rayOrigin.position + (Vector3)(dirNormalized * detectionDistance));
        // 왼쪽 방향
        Gizmos.DrawLine(rayOrigin.position, rayOrigin.position + (Vector3)(-dirNormalized * detectionDistance));
    }
}
