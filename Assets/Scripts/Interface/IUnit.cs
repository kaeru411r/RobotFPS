/// <summary>
/// ���j�b�g���p������C���^�[�t�F�[�X
/// </summary>
public interface IUnitFeature : IPause
{
    /// <summary>
    /// �@�̂Ƀ��j�b�g�𑕔�����
    /// </summary>
    /// <param name="robot"></param>
    public void Attach(RobotBase robot, Mount mount);

    /// <summary>
    /// �@�̂��烆�j�b�g���O��
    /// </summary>
    public void Detach();
}
