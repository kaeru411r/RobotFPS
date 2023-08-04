/// <summary>
/// 機能の一時停止、再開をする
/// </summary>
public interface IPause
{
    /// <summary>
    /// 機能を一時停止する
    /// </summary>
    public void Pause();
    /// <summary>
    /// 機能を再開する
    /// </summary>
    public void Resume();
}
