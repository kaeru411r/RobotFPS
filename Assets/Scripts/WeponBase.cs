using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����̃x�[�X�N���X
/// </summary>
public abstract class WeponBase : UnitBase, IWepon
{
    /// <summary>
    /// �����̑���t�F�[�Y</summary>
    public enum Phase
    {
        Started,
        Performed,
        Canceled
    }

    public override void Set(RobotBase robot)
    {
        robot.AddWepon(this);
    }
    public abstract void Fire(Phase phase);
    public abstract void Aim(Phase phase);
    public abstract void Reroad(Phase phase);
}