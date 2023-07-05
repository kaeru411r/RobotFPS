using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��e���Ɏ󂯂�_���[�W�ɕ␳��������
/// </summary>
public class DamageFactor : UnitFeatureBase
{
    [SerializeField, Tooltip("�_���[�W�{��")]
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
