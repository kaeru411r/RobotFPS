using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRate : UnitBase
{
    [SerializeField, Tooltip("�ˌ����[�g�{��")]
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
        if (_isAttach)
        {
            _robot.FireRateFactor /= _rateFactor;
        }
    }

    protected override void OnResume()
    {
        if (_isAttach)
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
