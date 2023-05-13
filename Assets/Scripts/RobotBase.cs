using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// �@�̂̑����Ǘ��N���X
/// ��{�I�ɋ@�̂̂قƂ�ǂ̂��������̃N���X����čs��
/// </summary>
public class RobotBase : MonoBehaviour, IWepon
{
    [SerializeField, Tooltip("�}�E���g")]
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

    public int WeponNumber { get => _weponNumber; set => _weponNumber = value; }

    private void Awake()
    {
        MountsInit();
    }

    /// <summary>�����o�^</summary>
    /// <param name="wepon"></param>
    public void AddWepon(WeponBase wepon)
    {
        _wepons.Add(wepon);
    }

    /// <summary>�����폜</summary>
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