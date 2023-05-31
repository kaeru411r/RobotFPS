using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 被弾時に受けるダメージに補正をかける
/// </summary>
public class DamageFactor : UnitBase
{
    [SerializeField, Tooltip("ダメージ倍率")]
    float _damageFactor;

    RobotBase _robot;
    public override void Attach(RobotBase robot)
    {
        _robot = robot;
        robot.OnDamageFuncs.Add(Factor);
    }

    public override void Detach()
    {
        _robot?.OnDamageFuncs.Remove(Factor);
    }

    public override void Pause()
    {
    }

    public override void Resume()
    {
    }

    public float Factor(float baseDamage)
    {
        return baseDamage * _damageFactor;
    }
}
