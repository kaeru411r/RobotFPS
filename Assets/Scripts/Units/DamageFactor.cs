using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFactor : UnitBase
{
    [SerializeField, Tooltip("É_ÉÅÅ[ÉWî{ó¶")]
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
