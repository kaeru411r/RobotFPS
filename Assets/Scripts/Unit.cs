using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// このクラスを継承するクラスは
/// OnEnableの初めにOnEnableを、
/// OnDisable、OnDestroyの最後にOnDisable、OnDestroyを呼ぶこと
/// </summary>
public class Unit : MonoBehaviour, IConfigurable
{
    [SelectableSerializeReference, SerializeReference, Tooltip("機能一覧")]
    public IUnitFeature[] _features;

    RobotBase _robot;



    /// <summary>
    /// 機体にユニットを装備する
    /// </summary>
    /// <param name="robot"></param>
    public void Attach(RobotBase robot, Mount mount)
    {
        _robot = robot;
        for (var i = 0; i < _features.Length; i++)
        {
            if (_features[i] != null)
            {
                _features[i]?.Attach(robot, mount);
            }
        }
    }


    public void Detach()
    {
        for (var i = 0; i < _features.Length; i++)
        {
            if (_features[i] != null)
            {
                _features[i]?.Detach();
            }
        }
        _robot = null;
    }

    public void Pause()
    {
        for (var i = 0; i < _features.Length; i++)
        {
            if (_features[i] != null)
            {
                _features[i]?.Pause();
            }
        }
    }

    public void Resume()
    {
        for (var i = 0; i < _features.Length; i++)
        {
            if (_features[i] != null)
            {
                _features[i]?.Resume();
            }
        }
    }

    public string Seve()
    {
        var config = new Config();
        foreach(var feature in _features)
        {
            var featureConfig = "";
            if(feature != null)
            {
                featureConfig = feature.Seve();
            }
            config.Add(featureConfig);
        }
        return JsonUtility.ToJson(config);
    }

    public void Load(string config) { }

    private void OnEnable()
    {
        for (var i = 0; i < _features.Length; i++)
        {
            if (_features[i] != null)
            {
                _features[i]?.Resume();
            }
        }
    }

    private void OnDisable()
    {

        for (var i = 0; i < _features.Length; i++)
        {
            if (_features[i] != null)
            {
                _features[i]?.Pause();
            }
        }
    }

    private void OnDestroy()
    {
        if (_robot)
        {

            for (var i = 0; i < _features.Length; i++)
            {
                if (_features[i] != null)
                {
                    _features[i]?.Detach();
                }
            }
        }
    }

    [Serializable]
    public class Config
    {
        public List<string> Values = new List<string>();

        public Config() { }

        public Config(string[] values)
        {
            Values = values.ToList();
        }

        public void Add(string value)
        {
            Values.Add(value);
        }
    }
}
