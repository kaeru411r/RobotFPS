using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// �@�̂̑����Ǘ��N���X
/// ��{�I�ɋ@�̂̂قƂ�ǂ̂��������̃N���X����čs��
/// </summary>
[RequireComponent(typeof(HitPoint))]
public class RobotBase : MonoBehaviour, IWepon
{
    [SerializeField, Tooltip("�}�E���g")]
    Mount[] _mounts = new Mount[0];
    [SerializeField, Tooltip("�ő�̗�")]
    int _maxHp = 0;

    List<WeponBase> _wepons = new List<WeponBase>();
    int _weponNumber = 0;
    HitPoint _hp;
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

    private void Awake()
    {
        Init();
    }


    public void Init()
    {
        _hp = GetComponent<HitPoint>();
        _hp.HPReset(HpReset(_maxHp));
        _hp.OnDownAction += Down;
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