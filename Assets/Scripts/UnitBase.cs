using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ユニットのベースクラス
/// </summary>
public abstract class UnitBase : MonoBehaviour, IPause
{

    protected bool _isAttach { get; private set; } = false;
    protected RobotBase _robot { get; private set; } = null;
    protected bool _isPause { get; private set; } = false;

    Mount _mount;
    /// <summary>
    /// 機体にユニットを装備する
    /// </summary>
    /// <param name="robot"></param>
    public void Attach(RobotBase robot, Mount mount)
    {
        _isAttach = true;
        _robot = robot;
        _mount = mount;
        Debug.Log($"アタッチ, {name}, {_mount.Name}");
        OnAttach();
    }


    public void Detach()
    {
        OnDetach();
        Debug.Log($"デタッチ, {name}, {_mount.Name}");
        _isAttach = false;
        _robot = null;
    }

    public void Pause()
    {
        if (_isPause) return;
        _isPause = true;
        OnPause();
    }

    public void Resume()
    {
        if (!_isPause) return;
        _isPause = false;
        OnResume();
    }

    protected virtual void OnAttach() { }
    protected virtual void OnDetach() { }
    protected virtual void OnPause() { }
    protected virtual void OnResume() { }


    private void OnEnable()
    {
        Resume();
    }
    private void OnDisable()
    {
        Pause();
    }
    private void OnDestroy()
    {
        if (_robot)
        {
            Detach();
        }
    }
}
