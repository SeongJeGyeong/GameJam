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
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin.position, direction.normalized, detectionDistance, playerLayer);
        return hit.collider != null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        if (rayOrigin != null)
        {
            Gizmos.DrawLine(rayOrigin.position, rayOrigin.position + (Vector3)(direction.normalized * detectionDistance));
        }
    }
}
