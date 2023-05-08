using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武装のベースクラス
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


    //---------------テスト用--------------------
    public  void Fire(int phase)
    {
        Fire((WeponActionPhase)phase);
    }
    public  void Aim(int phase)
    {
        Aim((WeponActionPhase)phase);
    }
    public  void Reroad(int phase)
    {
        Reroad((WeponActionPhase)phase);
    }
    //--------------------------------------------
}