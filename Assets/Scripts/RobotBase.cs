using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RobotBase : MonoBehaviour
{
    [SerializeField]
    Mount[] _mounts = new Mount[0];

    List<IWepon> _wepons = new List<IWepon>();


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

    public void AddWepon(IWepon wepon)
    {
        _wepons.Add(wepon);
    }

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
