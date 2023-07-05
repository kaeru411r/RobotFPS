using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武装のベースクラス
/// </summary>
public abstract class WeponBase : UnitFeatureBase, IWepon
{

    protected override void OnAttach()
    {
        _robot.AddWepon(this);
    }
    protected override void OnDetach()
    {
        _robot?.RemoveWepon(this);
    }

    public abstract void OnFire(WeponActionPhase phase);
    public abstract void OnAim(WeponActionPhase phase);
    public abstract void OnReload(WeponActionPhase phase);
    public abstract void OnTargeting(TargetingData data);

#if UNITY_EDITOR
    //---------------テスト用--------------------
    public void Fire(int phase)
    {
        OnFire((WeponActionPhase)phase);
    }
    public void Aim(int phase)
    {
        OnAim((WeponActionPhase)phase);
    }
    public void Reload(int phase)
    {
        OnReload((WeponActionPhase)phase);
    }

    //--------------------------------------------
#endif
}