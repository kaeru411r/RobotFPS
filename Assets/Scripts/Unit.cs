using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// このクラスを継承するクラスは
/// OnEnableの初めにOnEnableを、
/// OnDisable、OnDestroyの最後にOnDisable、OnDestroyを呼ぶこと
/// </summary>
public class Unit : MonoBehaviour, IIDHolder
{
    [SelectableSerializeReference, SerializeReference, Tooltip("機能一覧")]
    public IUnitFeature[] _features;
    [SerializeField]
    ID _id;

    RobotBase _robot;

    public ID ID => _id;



    /// <summary>
    /// 機体にユニットを装備する
    /// </summary>
    /// <param name="robot"></param>
    public void Attach(RobotBase robot, Mount mount)
    {
        _robot = robot;
        for (int i = 0; i < _features.Length; i++)
        {
            if (_features[i] != null)
            {
                _features[i]?.Attach(robot, mount);
            }
        }
    }


    public void Detach()
    {
        for (int i = 0; i < _features.Length; i++)
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
        for (int i = 0; i < _features.Length; i++)
        {
            if (_features[i] != null)
            {
                _features[i]?.Pause();
            }
        }
    }

    public void Resume()
    {
        for (int i = 0; i < _features.Length; i++)
        {
            if (_features[i] != null)
            {
                _features[i]?.Resume();
            }
        }
    }

    private void OnEnable()
    {
        for (int i = 0; i < _features.Length; i++)
        {
            if (_features[i] != null)
            {
                _features[i]?.Resume();
            }
        }
    }

    private void OnDisable()
    {

        for (int i = 0; i < _features.Length; i++)
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

            for (int i = 0; i < _features.Length; i++)
            {
                if (_features[i] != null)
                {
                    _features[i]?.Detach();
                }
            }
        }
    }

}
