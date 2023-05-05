using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WeponBase;

public interface IWepon
{
    public void Fire(Phase phase);
    public void Aim(Phase phase);
    public void Reroad(Phase phase);
}
