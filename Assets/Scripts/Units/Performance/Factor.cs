using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factor : UnitFeatureBase
{
    [SerializeField, Tooltip("")]
    Value[] _values = new Value[0];


    protected override void OnAttach()
    {
        foreach (var v in _values)
        {
            GetRef(v.type) *= v.value;
        }
    }
    protected override void OnDetach()
    {
        foreach (var v in _values)
        {
            GetRef(v.type) /= v.value;
        }
    }

    protected override void OnPause()
    {
        if (_isAttach)
        {
            foreach (var v in _values)
            {
                GetRef(v.type) /= v.value;
            }
        }
    }

    protected override void OnResume()
    {
        if (_isAttach)
        {
            foreach (var v in _values)
            {
                GetRef(v.type) *= v.value;
            }
        }
    }

    ref float GetRef(Type type)
    {
        switch (type)
        {
            case Type.Atk:
                return ref _robot.Performance.AtkFactor;
            case Type.Def:
                return ref _robot.Performance.DefFactor;
            case Type.Speed:
                return ref _robot.Performance.SpeedFactor;
            case Type.Rate:
                return ref _robot.Performance.RateFactor;
            default:
                throw new NotImplementedException();
        }
    }


    private void OnValidate()
    {
        foreach (var v in _values)
        {
            if(v.value < 0)
            {
                v.value = 0;
            }
        }
    }


    [Serializable]
    public class Value
    {
        public float value = 1;
        public Type type;
    }

    public enum Type
    {
        Atk,
        Def,
        Speed,
        Rate,
    }
}

