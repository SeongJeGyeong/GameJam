public interface IPlayerDetector
{
    /// <summary>
    /// 플레이어를 감지했는가?
    /// </summary>
    /// <returns>플레이어가 범위 내에 있으면 true</returns>
    bool IsPlayerDetected();
}