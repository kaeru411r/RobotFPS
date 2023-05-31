using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �̗͂��Ǘ�����
/// </summary>
public class HitPoint : MonoBehaviour
{
    [SerializeField, Tooltip("�ő�̗�")]
    int _maxHP;
    [SerializeField, Tooltip("�̗�")]
    int _hp;

    Action _onDownAction;

    public int MaxHP { get => _maxHP; set => _maxHP = value; }


    public int HP { get => _hp; set => _hp = value; }
    public Action OnDownAction { get => _onDownAction; set => _onDownAction = value; }

    public void HPReset()
    {
        _hp = _maxHP;
    }
    public void HPReset(int maxHP)
    {
        _maxHP = maxHP;
        _hp = _maxHP;
    }


    public AttackResult Damage(int value)
    {
        _hp -= value;

        var isKilled = false;
        if (_hp <= 0)
        {
            isKilled = true;
            _hp = 0;
            _onDownAction?.Invoke();
        }

        return new AttackResult(value, isKilled);
    }
}
