using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventUnitBase : UnitBase
{

    RobotBase _robot;

    public override void Attach(RobotBase robot)
    {
        _robot = robot;
        robot.AddOnDamage(OnDamage);
    }

    public override void Detach()
    {
        _robot.RemoveOnDamage(OnDamage);
    }

    public int OnDamage(int damage)
    {
        return damage / 2;
    }

    public override void Pause()
    {
        throw new NotImplementedException();
    }

    public override void Resume()
    {
        throw new NotImplementedException();
    }
}
