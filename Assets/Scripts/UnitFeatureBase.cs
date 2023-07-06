using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���j�b�g�̃x�[�X�N���X
/// </summary>
public abstract class UnitFeatureBase : IPause, IUnitFeature
{

    protected bool _isAttach { get; private set; } = false;
    protected RobotBase _robot { get; private set; } = null;
    protected bool _isPause { get; private set; } = false;
    protected GameObject _gameObject { get; private set; }
    protected MonoBehaviour _mono {  get; private set; }

    public MonoBehaviour Mono { get => _mono; }

    [SerializeField]
    Guid _id;

    Mount _mount;

    /// <summary>
    /// �@�̂Ƀ��j�b�g�𑕔�����
    /// </summary>
    /// <param name="robot"></param>
    public void Attach(RobotBase robot, Mount mount)
    {
        _isAttach = true;
        _robot = robot;
        _mount = mount;
        Debug.Log($"�A�^�b�`, {_mono.name}, {_mount.Name}");
        OnAttach();
    }


    public void Detach()
    {
        OnDetach();
        Debug.Log($"�f�^�b�`, {_mono.name}, {_mount.Name}");
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