using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// �@�̂̑����Ǘ��N���X
/// ��{�I�ɋ@�̂̂قƂ�ǂ̂��������̃N���X����čs��
/// </summary>
public class RobotBase : MonoBehaviour, IWepon
{
    [SerializeField, Tooltip("�}�E���g")]
    Mount[] _mounts = new Mount[0];

    List<WeponBase> _wepons = new List<WeponBase>();
    int _weponNumber = 0;
    List<Func<int, int>> _onDamageFuncs = new List<Func<int, int>>();
    List<Action<WeponActionPhase>> _onFireActions = new List<Action<WeponActionPhase>>();
    List<Action<WeponActionPhase>> _onAimActions = new List<Action<WeponActionPhase>>();
    List<Action<WeponActionPhase>> _onReloadActions = new List<Action<WeponActionPhase>>();

    public WeponBase Wepon
    {
        get
        {
            if (_wepons == null || _wepons.Count <= _weponNumber) { return null; }
            return _wepons[_weponNumber];
        }
    }


    /// <summary>�}�E���g</summary>
    public Mount[] Mounts
    {
        get => _mounts;
        set
        {
            _mounts = value;
            MountsInit();
        }
    }

    public int WeponNumber
    {
        get => _weponNumber;
        set
        {
            Debug.Log(1);
            if (_wepons.Count > 0 && _wepons.Count > value)
            {
                Debug.Log(2);
                if (Wepon)
                {
                    Debug.Log(3);
                    _onFireActions.Remove(Wepon.OnFire);
                    _onAimActions.Remove(Wepon.OnAim);
                    _onReloadActions.Remove(Wepon.OnReload);
                }
                _weponNumber = value;
                if (Wepon)
                {
                    Debug.Log(4);
                    _onFireActions.Add(Wepon.OnFire);
                    _onAimActions.Add(Wepon.OnAim);
                    _onReloadActions.Add(Wepon.OnReload);
                }
            }
        }
    }
    public List<Func<int, int>> OnDamageEvent { get => _onDamageFuncs; }

    private void Awake()
    {
        MountsInit();
    }

    /// <summary>�����o�^</summary>
    /// <param name="wepon"></param>
    public void AddWepon(WeponBase wepon)
    {
        _wepons.Add(wepon);
        if (_wepons.Count == 1)
        {
            WeponNumber = 0;
        }
    }

    /// <summary>�����폜</summary>
    /// <param name="wepon"></param>
    public void RemoveWepon(WeponBase wepon)
    {
        _wepons.Remove(wepon);
        if(WeponNumber >= _wepons.Count)
        {
            WeponNumber = _wepons.Count - 1;
        }
    }

    public void AddOnDamage(Func<int, int> func)
    {
        _onDamageFuncs.Add(func);
    }
    public void RemoveOnDamage(Func<int, int> func)
    {
        _onDamageFuncs.Remove(func);
    }


    void MountsInit()
    {
        foreach (var mount in _mounts)
        {
            if (!mount.IsInitialized)
            {
                mount.Init(this);
            }
        }
    }

    public void OnFire(WeponActionPhase phase)
    {
        if (Wepon != null)
        {

            _onFireActions?.ForEach(a => a.Invoke(phase));
        }
    }

    public void OnAim(WeponActionPhase phase)
    {
        if (Wepon != null)
        {
            _onAimActions?.ForEach(a => a.Invoke(phase));
        }
    }

    public void OnReload(WeponActionPhase phase)
    {
        if (Wepon != null)
        {
            _onReloadActions?.ForEach(a => a.Invoke(phase));
        }
    }

    public int OnDamage(int damage)
    {
        _onDamageFuncs?.ForEach(f => damage = f(damage));

        return damage;
    }
}