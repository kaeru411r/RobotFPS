using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>���������{�b�g�ɑ�������</summary>
[System.Serializable]
public class Mount
{
    [SerializeField]
    UnitBase _unit = null;
    [SerializeField]
    GameObject _mountBase = null;

    RobotBase _robot = null;

    bool _isInitialized = false;

    public UnitBase Unit
    {
        get => _unit;
        set
        {
            _unit = value;
            UnitSet();
        }
    }
    public GameObject MountBase { get => _mountBase; }
    public RobotBase Robot
    {
        get
        {
            if (!_isInitialized)
            {
                Debug.LogError("���������ς�ł��܂���");
            }
            return _robot;
        }
    }

    public bool IsInitialized { get => _isInitialized; }

    public void Init(RobotBase robot)
    {
        _robot = robot;
        _isInitialized = true;
        UnitSet();
    }

    void UnitSet()
    {
        if (Robot != null &&_unit != null)
        {
            _unit.Set(Robot);
        }
    }
}
