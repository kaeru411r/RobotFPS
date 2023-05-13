using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武装のベースクラス
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
        _robot.RemoveWepon(this);
    }
    public abstract void OnFire(WeponActionPhase phase);
    public abstract void OnAim(WeponActionPhase phase);
    public abstract void OnReload(WeponActionPhase phase);


    //---------------テスト用--------------------
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