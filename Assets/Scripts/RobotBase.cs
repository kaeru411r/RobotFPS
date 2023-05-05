using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 機体の装備管理クラス
/// 基本的に機体のほとんどのやり取りをこのクラスを介して行う
/// </summary>
public class RobotBase : MonoBehaviour
{
    [SerializeField, Tooltip("マウント")]
    Mount[] _mounts = new Mount[0];

    List<IWepon> _wepons = new List<IWepon>();


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

    private void Awake()
    {
        MountsInit();
    }

    /// <summary>武装登録</summary>
    /// <param name="wepon"></param>
    public void AddWepon(IWepon wepon)
    {
        _wepons.Add(wepon);
    }

    /// <summary>武装削除</summary>
    /// <param name="wepon"></param>
    public void RemoveWepon(IWepon wepon)
    {
        _wepons.Remove(wepon);
    }


    void MountsInit()
    {
        foreach (var mount in _mounts)
        {
            if (mount.IsInitialized)
            {
                mount.Init(this);
            }
        }
    }
}
