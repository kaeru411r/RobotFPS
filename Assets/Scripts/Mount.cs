using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;


/// <summary>装備をロボットに装着する</summary>
[System.Serializable]
public class Mount : IPause, IConfigurable
{
    [SerializeField, Tooltip("名前")]
    string _name;
    [SerializeField, Tooltip("装備できるユニットと、その接続パーツのリスト")]
    Unit[] _supportedUnits;

    [SerializeField, HideInInspector]
    bool _isInitialized = false;
    [SerializeField, HideInInspector]
    Unit _unit;

    RobotBase _robot = null;
    GameObject _mountBase = null;




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
    public Unit Unit
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
    public Unit[] SupportedUnits { get => _supportedUnits; set => _supportedUnits = value; }


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

    public string Seve()
    {
        var config = new Config();
        for (var i = 0; i < _supportedUnits.Length; i++)
        {
            var unitConfig = "";
            if (_supportedUnits[i] != null)
            {
                unitConfig = _supportedUnits[i].Seve();
            }
            config.Add(unitConfig);

            if (_supportedUnits[i] == _unit)
            {
                config.UseUnitIndex = i;
            }
        }
        return JsonUtility.ToJson(config);
    }

    public void Load(string json)
    {
        var data = JsonUtility.FromJson<Config>(json);
        _unit = _supportedUnits[data.UseUnitIndex];
        for (var i = 0; i < _supportedUnits.Length; i++)
        {
            _supportedUnits[i]?.Load(data.Settings[i]);
        }
    }

    void UnitSet()
    {
        //_unit = GameObject.Instantiate(_unit);
        if (Robot != null && _unit != null)
        {
            _unit.Attach(Robot, this);
        }
    }


    [Serializable]
    public class Config
    {
        public int UseUnitIndex;
        public List<string> Settings;

        public Config(int index, string[] settings)
        {
            UseUnitIndex = index;
            Settings = settings.ToList();
        }

        public Config(int index)
        {
            UseUnitIndex = index;
            Settings = new List<string>();
        }
        public Config(int index, List<string> settings)
        {
            UseUnitIndex = index;
            Settings = settings;
        }

        public Config()
        {
            Settings = new List<string>();
        }

        public void Add(string value)
        {
            Settings.Add(value);
        }

    }

}
