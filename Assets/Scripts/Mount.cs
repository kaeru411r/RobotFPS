using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>装備をロボットに装着する</summary>
[System.Serializable]
public class Mount
{
    [SerializeField, Tooltip("ユニット")]
    UnitBase _unit = null;
    [SerializeField, Tooltip("装備できるユニットと、その接続パーツのリスト")]
    UnitBase[] _supportedUnits;

    RobotBase _robot = null;
    GameObject _mountBase = null;

    bool _isInitialized = false;


    /// <summary>ユニット基部</summary>
    public GameObject MountBase { get => _mountBase; }

    /// <summary></summary>
    public RobotBase Robot
    {
        get
        {
            if (!_isInitialized)
            {
                Debug.LogError("初期化が済んでいません");
            }
            return _robot;
        }
    }

    /// <summary>初期化が済んでいるか</summary>
    public bool IsInitialized { get => _isInitialized; }


    /// <summary>
    /// 初期化
    /// 装備する機体を渡す
    /// </summary>
    /// <param name="robot"></param>
    public void Init(RobotBase robot)
    {
        _robot = robot;
        _isInitialized = true;
        UnitSet();
    }

    void UnitSet()
    {
        if (Robot != null && _unit != null)
        {
            _unit.Attach(Robot);
        }
    }

}
