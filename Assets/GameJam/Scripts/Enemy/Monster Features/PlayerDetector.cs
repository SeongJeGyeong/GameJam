using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour, IPlayerDetectable
{
    [Header("감지 설정")]
    // Inspector에서 슬라이더로 조절
    [Range(0f, 50f)]
    [SerializeField] private float detectionRadius; 
    //[Range(0f, 100f)]
    //[SerializeField] private float chasingOutRadius; 
    
    
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform detectionCenter;

    private Transform detectedPlayerTransform; // 감지된 플레이어 Transform 저장

    private void Reset()
    {
        detectionCenter = transform;
    }

    public bool IsPlayerDetected()
    {
        Collider2D hit = Physics2D.OverlapCircle(detectionCenter.position, detectionRadius, playerLayer);

        if (hit != null)
        {
            detectedPlayerTransform = hit.transform;
            return true;
        }

        detectedPlayerTransform = null; // 감지 안 됐으면 초기화
        return false;
    }

    public Transform GetPlayerTransform()
    {
        return detectedPlayerTransform;
    }

    private void OnDrawGizmosSelected()
    {
        if (detectionCenter == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(detectionCenter.position, detectionRadius);
    }
}
