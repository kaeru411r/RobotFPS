using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����̃x�[�X�N���X
/// </summary>
public abstract class WeponBase : UnitBase, IWepon
{
    RobotBase _robot;

    public override void Attach(RobotBase robot)
    {
        _robot = robot;
        robot.AddWepon(this);
    }
    public override void Detach()
    {
        _robot?.RemoveWepon(this);
    }
    public abstract void OnFire(WeponActionPhase phase);
    public abstract void OnAim(WeponActionPhase phase);
    public abstract void OnReload(WeponActionPhase phase);
    public abstract void OnTargeting(Vector3 target, TargetingMode targetingMode);


    //---------------�e�X�g�p--------------------
    public  void Fire(int phase)
    {
        OnFire((WeponActionPhase)phase);
    }
    public  void Aim(int phase)
    {
        OnAim((WeponActionPhase)phase);
    }
    public  void Reload(int phase)
    {
        OnReload((WeponActionPhase)phase);
    }
    //--------------------------------------------
}