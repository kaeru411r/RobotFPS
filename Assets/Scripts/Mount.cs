using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>装備をロボットに装着する</summary>
[System.Serializable]
public class Mount : IPause
{
    [SerializeField, Tooltip("名前")]
    string _name;
    [SerializeField, Tooltip("装備できるユニットと、その接続パーツのリスト")]
    UnitBase[] _supportedUnits;

    [SerializeField, HideInInspector]
    bool _isInitialized = false;

    RobotBase _robot = null;
    GameObject _mountBase = null;
    [SerializeField, HideInInspector]
    UnitBase _unit;


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
    public UnitBase Unit
    {
        get => _unit;
        set
        {
            if (_isInitialized)
            {
                if (_unit == value) { return; }
                _unit?.Detach();
                _unit?.gameObject.SetActive(false);
                if (value.gameObject.scene.buildIndex == -1)
                {
                    var go = GameObject.Instantiate(value);
                    go.name = value.name;
                    value = go;
                }
            }
            Debug.Log(value);
            _unit = value;
            if (_isInitialized)
            {
                _unit.gameObject.SetActive(true);
                _unit.Attach(Robot, this);
            }
        }
    }
    public string Name { get => _name; set => _name = value; }
    public UnitBase[] SupportedUnits { get => _supportedUnits; set => _supportedUnits = value; }


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

    public void Pause()
    {
        _unit.Pause();
    }

    public void Resume()
    {
        _unit.Resume();
    }

    void UnitSet()
    {
        if (Robot != null && _unit != null)
        {
            _unit.Attach(Robot, this);
        }
    }
}
