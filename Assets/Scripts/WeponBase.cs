using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeponBase : UnitBase, IWepon
{
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
