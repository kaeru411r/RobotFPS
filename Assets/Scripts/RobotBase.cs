using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// 機体の装備管理クラス
/// 基本的に機体のほとんどのやり取りをこのクラスを介して行う
/// </summary>
[RequireComponent(typeof(HitPoint), typeof(Movement))]
public class RobotBase : MonoBehaviour, IWepon
{
    [SerializeField, Tooltip("マウント")]
    Mount[] _mounts = new Mount[0];
    [SerializeField, Tooltip("最大体力")]
    int _maxHp = 0;
    [SerializeField, Tooltip("基本移動速度")]
    float _speed;
    [SerializeField, Tooltip("移動速度補正")]
    Vector2 _speedCorrection;
    [SerializeField, Tooltip("旋回速度")]
    float _turnSpeed;

    List<WeponBase> _wepons = new List<WeponBase>();
    int _weponNumber = 0;
    HitPoint _hp;
    Movement _movement;
    List<Func<float, float>> _onDamageFuncs = new List<Func<float, float>>();
    List<Action<WeponActionPhase>> _onFireActions = new List<Action<WeponActionPhase>>();
    List<Action<WeponActionPhase>> _onAimActions = new List<Action<WeponActionPhase>>();
    List<Action<WeponActionPhase>> _onReloadActions = new List<Action<WeponActionPhase>>();
    List<Action<Vector2>> _onMoveFuncs = new List<Action<Vector2>>();
    List<Action<Vector3, TargetingMode>> _onTargetingActions = new List<Action<Vector3, TargetingMode>>();
    List<Action<float>> _onTurnFuncs = new List<Action<float>>();
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
            if (_wepons.Count > 0 && _wepons.Count > value)
            {
                if (Wepon)
                {
                    _onFireActions.Remove(Wepon.OnFire);
                    _onAimActions.Remove(Wepon.OnAim);
                    _onReloadActions.Remove(Wepon.OnReload);
                }
                _weponNumber = value;
                if (Wepon)
                {
                    _onFireActions.Add(Wepon.OnFire);
                    _onAimActions.Add(Wepon.OnAim);
                    _onReloadActions.Add(Wepon.OnReload);
                }
            }
        }
    }
    public int HP { get => _hp.HP; }
    public List<Func<float, float>> OnDamageFuncs { get => _onDamageFuncs; }
    public List<Action<WeponActionPhase>> OnFireActions { get => _onFireActions; }
    public List<Action<WeponActionPhase>> OnAimActions { get => _onAimActions; }
    public List<Action<WeponActionPhase>> OnReloadActions { get => _onReloadActions; }
    public List<Func<int, int>> OnHpResetFuncs { get => _onHpResetFuncs; }
    public List<Action<Vector3, TargetingMode>> OnTargetingActions { get => _onTargetingActions;}

    private void Awake()
    {
        Init();
    }


    public void Init()
    {
        _hp = GetComponent<HitPoint>();
        _hp.HPReset(HpReset(_maxHp));
        _hp.OnDownAction += Down;
        _movement = GetComponent<Movement>();
        _movement.Speed = _speed;
        _movement.BackCorrection = _speedCorrection.y;
        _movement.SideCorrection = _speedCorrection.x;
        _movement.TurnSpeed = _turnSpeed;
        _onMoveFuncs.Add(_movement.Move);
        MountsInit();
    }

    /// <summary>武装登録</summary>
    /// <param name="wepon"></param>
    public void AddWepon(WeponBase wepon)
    {
        _wepons.Add(wepon);
        _onTargetingActions.Add(wepon.OnTargeting);
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
        _onTargetingActions.Remove(wepon.OnTargeting);
        if (WeponNumber >= _wepons.Count)
        {
            WeponNumber = _wepons.Count - 1;
        }
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


    #region ========== 行動入力部 =====================================


    public void OnFire(WeponActionPhase phase)
    {
        _onFireActions.ForEach(a => a.Invoke(phase));
    }

    public void OnAim(WeponActionPhase phase)
    {
        _onAimActions.ForEach(a => a.Invoke(phase));
    }

    public void OnReload(WeponActionPhase phase)
    {
        _onReloadActions.ForEach(a => a.Invoke(phase));
    }

    public void OnMove(Vector2 velocity)
    {
        _onMoveFuncs.ForEach(a => a.Invoke(velocity));
    }

    public void OnTurn(float turn)
    {
        _onTurnFuncs.ForEach(a => a.Invoke(turn));
    }

    public void OnTargeting(Vector3 angle, TargetingMode targetingMode)
    {
        _onTargetingActions.ForEach(a => a.Invoke(angle, targetingMode));
    }

    #endregion =====================================================

    public AttackResult OnDamage(int damage)
    {
        float buf = damage;
        _onDamageFuncs?.ForEach(f => buf = f(buf));

        var result = _hp.Damage((int)buf);

        return result;
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