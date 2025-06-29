using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour, IPlayerDetectable
{
    [Header("탐지 범위")]
    // Inspector���� �����̴��� ����
    [Range(0f, 50f)]
    [SerializeField] private float detectionRadius;
    //[Range(0f, 100f)]
    //[SerializeField] private float chasingOutRadius; 


    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform detectionCenter;

    private Transform detectedPlayerTransform; // ������ �÷��̾� Transform ����

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

        detectedPlayerTransform = null; // ���� �� ������ �ʱ�ȭ
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