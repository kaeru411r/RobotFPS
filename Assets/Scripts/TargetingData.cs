using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TargetingData
{
    Vector3 _forward;
    Vector3 _target;
    TargetingMode _targetingMode;

    public Vector3 Forward { get => _forward; set => _forward = value; }
    public Vector3 Target { get => _target; set => _target = value; }
    public TargetingMode TargetingMode { get => _targetingMode; set => _targetingMode = value; }


    public TargetingData(Vector3 forward, Vector3 target)
    {
        _forward = forward;
        _target = target;
        _targetingMode = TargetingMode.Position;
    }

    public TargetingData(Vector3 forward)
    {
        _forward = forward;
        _target = forward;
        _targetingMode = TargetingMode.Angle;
    }
}
