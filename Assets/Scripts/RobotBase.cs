using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;


/// <summary>
/// 機体の装備管理クラス
/// 基本的に機体のほとんどのやり取りをこのクラスを介して行う
/// </summary>
[RequireComponent(typeof(HitPoint))]
public class RobotBase : MonoBehaviour, IWepon, IPause
{
    [SerializeField, Tooltip("マウント")/*, HideInInspector*/]
    Mount[] _mounts = new Mount[0];
    [SerializeField, Tooltip("最大体力")]
    int _maxHp = 0;
    [SerializeField, Tooltip("基本移動速度")]
    float _speed;
    [SerializeField, Tooltip("移動速度補正")]
    Vector2 _speedCorrection;
    [SerializeField, Tooltip("旋回速度")]
    float _turnSpeed;
    [SerializeField, Tooltip("性能値")]
    Performance _performance = new Performance();

    List<IWepon> _wepons = new List<IWepon>();
    int _weponNumber = 0;
    HitPoint _hp;
    Movement _movement;
    List<Func<float, float>> _onDamageFuncs = new List<Func<float, float>>();
    List<Action<WeponActionPhase>> _onFireActions = new List<Action<WeponActionPhase>>();
    List<Action<WeponActionPhase>> _onAimActions = new List<Action<WeponActionPhase>>();
    List<Action<WeponActionPhase>> _onReloadActions = new List<Action<WeponActionPhase>>();
    List<Action<Vector2>> _onMoveFuncs = new List<Action<Vector2>>();
    List<Action<TargetingData>> _onTargetingActions = new List<Action<TargetingData>>();
    List<Action<Vector3>> _onTurnFuncs = new List<Action<Vector3>>();
    List<Func<int, int>> _onHpResetFuncs = new List<Func<int, int>>();
    List<Action> _onDown = new List<Action>();
    bool _isPause = false;

    public IWepon Wepon
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
            if (value < 0)
            {
                value = Mathf.Max(0, _wepons.Count - 1);
                //Debug.Log(value);
            }
            else if (value >= _wepons.Count)
            {
                value = 0;
                //Debug.Log(value);
            }
            RemoveWeponAction(Wepon);
            _weponNumber = value;
            if (Wepon != null)
            {
                _onFireActions.Add(Wepon.OnFire);
                _onAimActions.Add(Wepon.OnAim);
                _onReloadActions.Add(Wepon.OnReload);
            }
        }
    }
    public int HP { get => _hp.HP; }
    public List<Func<float, float>> OnDamageFuncs { get => _onDamageFuncs; }
    public List<Action<WeponActionPhase>> OnFireActions { get => _onFireActions; }
    public List<Action<WeponActionPhase>> OnAimActions { get => _onAimActions; }
    public List<Action<WeponActionPhase>> OnReloadActions { get => _onReloadActions; }
    public List<Func<int, int>> OnHpResetFuncs { get => _onHpResetFuncs; }
    public List<Action<TargetingData>> OnTargetingActions { get => _onTargetingActions; }
    public List<Action<Vector2>> OnMoveFuncs { get => _onMoveFuncs; }
    public List<Action<Vector3>> OnTurnFuncs { get => _onTurnFuncs; }
    public float TurnSpeed { get => _turnSpeed; set => _turnSpeed = value; }
    public Performance Performance { get => _performance; set => _performance = value; }
    public List<Action> OnDown { get => _onDown; set => _onDown = value; }

    public void Init()
    {
        GameManager.Instance.Pauses.Add(this);
        _hp = GetComponent<HitPoint>();
        _hp.HPReset(HpReset(_maxHp));
        _hp.OnDownAction += Down;
        //_movement = GetComponent<Movement>();
        //_movement.Speed = _speed;
        //_movement.BackCorrection = _speedCorrection.y;
        //_movement.SideCorrection = _speedCorrection.x;
        //_onMoveFuncs.Add(_movement.Move);
        MountsInit();
        return;
    }

    /// <summary>武装登録</summary>
    /// <param name="wepon"></param>
    public void AddWepon(IWepon wepon)
    {
        _wepons.Add(wepon);
        _onTargetingActions.Add(wepon.OnTargeting);
        if (_wepons.Count == 1)
        {
            WeponNumber = 0;
        }
        else
        {
            WeponNumber = WeponNumber;
        }
    }

    /// <summary>武装削除</summary>
    /// <param name="wepon"></param>
    public void RemoveWepon(IWepon wepon)
    {
        RemoveWeponAction(wepon);
        _wepons.Remove(wepon);
        _onTargetingActions.Remove(wepon.OnTargeting);
        if (WeponNumber >= _wepons.Count)
        {
            WeponNumber = _wepons.Count - 1;
        }
        else
        {
            WeponNumber = WeponNumber;
        }
    }

    void RemoveWeponAction(IWepon wepon)
    {
        if (wepon != null)
        {
            _onFireActions.Remove(wepon.OnFire);
            _onAimActions.Remove(wepon.OnAim);
            _onReloadActions.Remove(wepon.OnReload);
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
        if (_isPause) { return; }
        _onFireActions.ForEach(a => a.Invoke(phase));
    }

    public void OnAim(WeponActionPhase phase)
    {
        if (_isPause) { return; }
        _onAimActions.ForEach(a => a.Invoke(phase));
    }

    public void OnReload(WeponActionPhase phase)
    {
        if (_isPause) { return; }
        _onReloadActions.ForEach(a => a.Invoke(phase));
    }

    public void OnMove(Vector2 velocity)
    {
        if (_isPause) { return; }
        _onMoveFuncs.ForEach(a => a.Invoke(velocity));
    }

    public void OnTurn(Vector3 turn)
    {
        if (_isPause) { return; }
        _onTurnFuncs.ForEach(a => a.Invoke(turn));
    }

    public void OnTargeting(TargetingData data)
    {
        if (_isPause) { return; }
        float y = Mathf.Atan2(data.Forward.x, data.Forward.z) * Mathf.Rad2Deg;
        //transform.eulerAngles = new Vector3(0, y, 0);
        _onTurnFuncs.ForEach(f => f.Invoke(data.Forward));
        _onTargetingActions.ForEach(a => a?.Invoke(data));
    }

    public void Pause()
    {
        _isPause = true;
        Array.ForEach(Mounts, m => m.Pause());
    }

    public void Resume()
    {
        Array.ForEach(Mounts, m => m.Resume());
        _isPause = false;
    }
    #endregion =====================================================

    private void Awake()
    {
        Init();
    }

    private void OnDestroy()
    {
        GameManager.Instance.Pauses.Remove(this);
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
        _onDown.ForEach(e => e.Invoke());
        Destroy(gameObject);
    }

}