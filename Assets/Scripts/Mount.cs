using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>���������{�b�g�ɑ�������</summary>
[System.Serializable]
public class Mount
{
    [SerializeField, Tooltip("���j�b�g")]
    UnitBase _unit = null;
    [SerializeField, Tooltip("�����ł��郆�j�b�g�ƁA���̐ڑ��p�[�c�̃��X�g")]
    UnitBase[] _supportedUnits;

    RobotBase _robot = null;
    GameObject _mountBase = null;

    bool _isInitialized = false;


    /// <summary>���j�b�g�</summary>
    public GameObject MountBase { get => _mountBase; }

    /// <summary></summary>
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

    /// <summary>���������ς�ł��邩</summary>
    public bool IsInitialized { get => _isInitialized; }


    /// <summary>
    /// ������
    /// ��������@�̂�n��
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
