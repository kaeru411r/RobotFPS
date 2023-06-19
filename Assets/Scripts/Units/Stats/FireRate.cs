using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.CinemachineFreeLook;

public class FireRate : UnitBase
{
    [SerializeField, Tooltip("éÀåÇÉåÅ[Égî{ó¶")]
    float _rateFactor = 1;

    protected override void OnAttach()
    {
        _robot.FireRateFactor *= _rateFactor;
    }

    protected override void OnDetach()
    {
        _robot.FireRateFactor /= _rateFactor;
    }

    protected override void OnPause()
    {
        if (_isAttached)
        {
            _robot.FireRateFactor /= _rateFactor;
        }
    }

    protected override void OnResume()
    {
        if (_isAttached)
        {
            _robot.FireRateFactor *= _rateFactor;
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
