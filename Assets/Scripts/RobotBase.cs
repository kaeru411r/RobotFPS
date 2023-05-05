using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// �@�̂̑����Ǘ��N���X
/// ��{�I�ɋ@�̂̂قƂ�ǂ̂��������̃N���X����čs��
/// </summary>
public class RobotBase : MonoBehaviour
{
    [SerializeField, Tooltip("�}�E���g")]
    Mount[] _mounts = new Mount[0];

    List<IWepon> _wepons = new List<IWepon>();


    /// <summary>�}�E���g</summary>
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

    /// <summary>�����o�^</summary>
    /// <param name="wepon"></param>
    public void AddWepon(IWepon wepon)
    {
        _wepons.Add(wepon);
    }

    /// <summary>�����폜</summary>
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
