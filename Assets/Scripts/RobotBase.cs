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

    List<WeponBase> _wepons = new List<WeponBase>();
    int _weponNumber = 0;

    public WeponBase Wepon
    {
        get
        {
            if (_wepons == null) { return null; }
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

    public int WeponNumber { get => _weponNumber; set => _weponNumber = value; }

    private void Awake()
    {
        MountsInit();
    }

    /// <summary>武装登録</summary>
    /// <param name="wepon"></param>
    public void AddWepon(WeponBase wepon)
    {
        _wepons.Add(wepon);
    }

    /// <summary>武装削除</summary>
    /// <param name="wepon"></param>
    public void RemoveWepon(WeponBase wepon)
    {
        _wepons.Remove(wepon);
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
            Wepon.OnFire(phase);
        }
    }

    public void OnAim(WeponActionPhase phase)
    {
        if (Wepon != null)
        {
            Wepon.OnAim(phase);
        }
    }

    public void OnReload(WeponActionPhase phase)
    {
        if (Wepon != null)
        {
            Wepon.OnReload(phase);
        }
    }

}