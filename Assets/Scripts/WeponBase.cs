using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����̃x�[�X�N���X
/// </summary>
public abstract class WeponBase : UnitBase, IWepon
{
    public override void Set(RobotBase robot)
    {
        robot.AddWepon(this);
    }
    public abstract void Fire(WeponActionPhase phase);
    public abstract void Aim(WeponActionPhase phase);
    public abstract void Reroad(WeponActionPhase phase);
}