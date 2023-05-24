using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// 機体の装備管理クラス
/// 基本的に機体のほとんどのやり取りをこのクラスを介して行う
/// </summary>
public class RobotBase : MonoBehaviour, IWepon
{
    [SerializeField, Tooltip("マウント")]
    Mount[] _mounts = new Mount[0];
    [SerializeField, Tooltip("最大体力")]
    int _maxHp = 0;

    List<WeponBase> _wepons = new List<WeponBase>();
    int _weponNumber = 0;
    int _hp = 0;
    List<Func<float, float>> _onDamageFuncs = new List<Func<float, float>>();
    List<Action<WeponActionPhase>> _onFireActions = new List<Action<WeponActionPhase>>();
    List<Action<WeponActionPhase>> _onAimActions = new List<Action<WeponActionPhase>>();
    List<Action<WeponActionPhase>> _onReloadActions = new List<Action<WeponActionPhase>>();
    List<Func<int, int>> _onHpResetFuncs = new List<Func<int, int>>();

    public WeponBase Wepon
    {
        get
        {
            if (_wepons == null || _wepons.Count <= _weponNumber) { return null; }
            return _wepons[_weponNumber];
        }
    }


    /// <summary>マウント</summary>
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
    public int Hp { get => _hp;}

    private void Awake()
    {
        Init();
    }


    public void Init()
    {
        _hp = HpReset(_maxHp);
        MountsInit();
    }

    /// <summary>武装登録</summary>
    /// <param name="wepon"></param>
    public void AddWepon(WeponBase wepon)
    {
        _wepons.Add(wepon);
        if (_wepons.Count == 1)
        {
            WeponNumber = 0;
        }
    }

    /// <summary>武装削除</summary>
    /// <param name="wepon"></param>
    public void RemoveWepon(WeponBase wepon)
    {
        _wepons.Remove(wepon);
        if(WeponNumber >= _wepons.Count)
        {
            WeponNumber = _wepons.Count - 1;
        }
    }

    public void AddOnDamage(Func<float, float> func)
    {
        _onDamageFuncs.Add(func);
    }
    public void RemoveOnDamage(Func<float, float> func)
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

    public AttackResult OnDamage(int damage)
    {
        float buf = damage;
        _onDamageFuncs?.ForEach(f => buf = f(buf));

        var result = (int) buf;
        _hp -= result;

        var isKilled = false;
        if(_hp <= 0)
        {
            isKilled = true;
            Down();
        }

        return new AttackResult(result, isKilled);
    }


    int HpReset(int baseValue)
    {

        _onHpResetFuncs?.ForEach(f => baseValue = f(baseValue));
        return baseValue;
    }

    void Down()
    {
        Debug.Log("Down");
    }
}