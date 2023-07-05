using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRate : UnitFeatureBase
{
    [SerializeField, Tooltip("éÀåÇÉåÅ[Égî{ó¶")]
    float _rateFactor = 1;

    protected override void OnAttach()
    {
        _robot.Performance.RateFactor *= _rateFactor;
    }

    protected override void OnDetach()
    {
        _robot.Performance.RateFactor /= _rateFactor;
    }

    protected override void OnPause()
    {
        if (_isAttach)
        {
            _robot.Performance.RateFactor /= _rateFactor;
        }
    }

    protected override void OnResume()
    {
        if (_isAttach)
        {
            _robot.Performance.RateFactor *= _rateFactor;
        }
    }


    private void OnValidate()
    {
        if(_rateFactor < 0)
        {
            _rateFactor = 0;
        }
    }
}
