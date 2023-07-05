using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SelectableSerializeReference, SerializeReference, Tooltip("�@�\�ꗗ")]
    IUnitFeature[] _features;
    [SerializeField, ReadOnly, Tooltip("ID")]
    string _idPreview;

    Mount _mount;
    Guid _id;

    /// <summary>
    /// �@�̂Ƀ��j�b�g�𑕔�����
    /// </summary>
    /// <param name="robot"></param>
    public void Attach(RobotBase robot, Mount mount)
    {
        for(int i = 0; i < _features.Length; i++)
        {
            if (_features[i] != null)
            {
                _features[i].Attach(robot, mount);
            }
        }
    }


    public void Detach()
    {
        for (int i = 0; i < _features.Length; i++)
        {
            if (_features[i] != null)
            {
                _features[i].Detach();
            }
        }
    }

    public void Pause()
    {
        for (int i = 0; i < _features.Length; i++)
        {
            if (_features[i] != null)
            {
                _features[i].Pause();
            }
        }
    }

    public void Resume()
    {
        for (int i = 0; i < _features.Length; i++)
        {
            if (_features[i] != null)
            {
                _features[i].Resume();
            }
        }
    }


    private void Reset()
    {
        _id = Guid.NewGuid();
        _idPreview = _id.ToString("D");
    }
}
