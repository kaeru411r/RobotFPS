using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : UnitFeatureBase
{
    [SerializeField, Tooltip("ë¨ìxï‚ê≥")]
    float _speed = 1;

    protected override void OnAttach()
    {
        _robot.Performance.SpeedFactor *= _speed;
    }
    protected override void OnDetach()
    {
        _robot.Performance.RateFactor /= _speed;
    }

    protected override void OnPause()
    {
        if (_isAttach)
        {
            _robot.Performance.RateFactor /= _speed;
        }
    }

    protected override void OnResume()
    {
        if (_isAttach)
        {
            _robot.Performance.RateFactor *= _speed;
        }
    }


    private void OnValidate()
    {
        if (_speed < 0)
        {
            _speed = 0;
        }
    }
}
