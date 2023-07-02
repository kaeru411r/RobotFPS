using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRobotCamera : MonoBehaviour, IPause
{
    [SerializeField, Tooltip("カメラ")]
    CinemachineFreeLook _virtualCamera;
    [SerializeField, Tooltip("ロボット")]
    RobotBase _robot;
    [SerializeField, Tooltip("カメラ速度")]
    Vector2 _speed = new Vector2(0.1f, 0.001f);


    CinemachineBrain _brain;
    List<Action<TargetingData>> _onTargetSets = new List<Action<TargetingData>>();
    Vector2 _look = Vector2.zero;
    bool _isPause = false;

    bool _isActive {
        get
        {
            if(_virtualCamera == null) { return false; }
            return _brain.ActiveVirtualCamera == (ICinemachineCamera)_virtualCamera;
        }
    }
    public List<Action<TargetingData>> OnTargetSets { get => _onTargetSets; }


    public void Look(Vector2 value)
    {
        if (_isActive && !_isPause)
        {
            _look += value;
        }
    }


    private void Start()
    {
        _brain = FindObjectOfType<CinemachineBrain>();
        if (_brain)
        {
            StartCoroutine(Targeting());
        }
        GameManager.Instance.Pauses.Add(this);
    }


    IEnumerator Targeting()
    {
        while (true)
        {
            if (_isActive)
            {
                _virtualCamera.m_XAxis.Value += _look.x * _speed.x;
                _virtualCamera.m_YAxis.Value -= _look.y * _speed.y;
                _look = Vector2.zero;
                var ray = Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f));
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    _onTargetSets.ForEach(a => a?.Invoke(new TargetingData(Camera.main.transform.forward, hit.point)));
                }
                else
                {
                    _onTargetSets.ForEach(a => a?.Invoke(new TargetingData(Camera.main.transform.forward)));
                }
            }
            yield return null;
        }
    }

    public void Pause()
    {
        _isPause = true;
    }

    public void Resume()
    {
        _isPause = false;
    }
}
