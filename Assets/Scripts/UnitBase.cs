using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���j�b�g�̃x�[�X�N���X
/// </summary>
public abstract class UnitBase : MonoBehaviour
{
    /// <summary>
    /// �@�̂Ƀ��j�b�g�𑕔�����
    /// </summary>
    /// <param name="robot"></param>
    public abstract void Set(RobotBase robot);
}
