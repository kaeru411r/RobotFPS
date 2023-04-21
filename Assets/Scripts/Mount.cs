using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>‘•”õ‚ğƒƒ{ƒbƒg‚É‘•’…‚·‚é</summary>
[System.Serializable]
public class Mount
{
    [SerializeField]
    UnitBase _unit = null;
    [SerializeField]
    GameObject _mountBase = null;

    RobotBase _robot = null;

    public int i;

    public UnitBase Unit { get => _unit; set => _unit = value; }
    public GameObject MountBase { get => _mountBase;}
    public RobotBase Robot { get => _robot;}
    public int I { get => i; set => i = value; }

    private Mount()
    {
        Debug.Log(2);
    }

    public Mount(RobotBase robot)
    {
        Debug.Log(1);
        _robot = robot;
    }
}
