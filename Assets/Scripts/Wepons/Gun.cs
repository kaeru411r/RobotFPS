using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : WeponBase
{
    [SerializeField, Tooltip("�U����")]
    int _atk;
    [SerializeField, Tooltip("�e��")]
    Transform _muzzle;

    public override void Aim(WeponActionPhase phase)
    {
    }

    public override void Fire(WeponActionPhase phase)
    {
        Debug.Log(phase);
    }

    public override void Reroad(WeponActionPhase phase)
    {
        throw new System.NotImplementedException();
    }
}
