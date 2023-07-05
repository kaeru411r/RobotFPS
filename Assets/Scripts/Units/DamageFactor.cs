using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 被弾時に受けるダメージに補正をかける
/// </summary>
public class DamageFactor : UnitFeatureBase
{
    [SerializeField, Tooltip("ダメージ倍率")]
    float _damageFactor;

    protected override void OnAttach()
    {
        _robot.OnDamageFuncs.Add(Factor);
    }

    protected override void OnDetach()
    {
        _robot?.OnDamageFuncs.Remove(Factor);
    }
    public float Factor(float baseDamage)
    {
        return baseDamage * _damageFactor;
    }
}
