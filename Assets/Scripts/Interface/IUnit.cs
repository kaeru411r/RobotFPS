/// <summary>
/// ユニットが継承するインターフェース
/// </summary>
public interface IUnitFeature : IPause
{
    /// <summary>
    /// 機体にユニットを装備する
    /// </summary>
    /// <param name="robot"></param>
    public void Attach(RobotBase robot, Mount mount);

    /// <summary>
    /// 機体からユニットを外す
    /// </summary>
    public void Detach();
}
