using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ユニットのベースクラス
/// </summary>
public abstract class UnitBase : MonoBehaviour
{
    protected bool _isAttached { get; private set; } = false;
    protected RobotBase _robot { get; private set; } = null;

    /// <summary>
    /// 機体にユニットを装備する
    /// </summary>
    /// <param name="robot"></param>
    public void Attach(RobotBase robot)
    {
        _isAttached = true;
        _robot = robot;
        OnAttach();
    }


    public void Detach()
    {
        OnDetach();
        _isAttached = false;
        _robot = null;
    }

    protected virtual void OnAttach() { }
    protected virtual void OnDetach() { }
    protected virtual void OnPause() { }
    protected virtual void OnResume() { }


    private void OnEnable()
    {
        OnResume();
    }
    private void OnDisable()
    {
        OnPause();
    }
    private void OnDestroy()
    {
        Detach();
    }
}
