using UnityEngine;

public interface IPlayerDetectable
{
    /// <summary>
    /// �÷��̾ �����ߴ°�?
    /// </summary>
    /// <returns>�÷��̾ ���� ���� ������ true</returns>
    bool IsPlayerDetected();
    Transform GetPlayerTransform(); // Ž���� �÷��̾� ��ġ ��ȯ
}