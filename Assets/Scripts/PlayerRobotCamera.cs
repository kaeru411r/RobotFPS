using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRobotCamera : MonoBehaviour
{
    [SerializeField, Tooltip("ÉJÉÅÉâ")]
    CinemachineVirtualCameraBase _virtualCamera;


    CinemachineBrain _brain;
    List<Action<TargetingData>> _onTargetSets = new List<Action<TargetingData>>();

    public List<Action<TargetingData>> OnTargetSets { get => _onTargetSets;}

    private void Start()
    {
        _brain = FindObjectOfType<CinemachineBrain>();
        if (_brain)
        {
            StartCoroutine(Targeting());
        }
    }


    IEnumerator Targeting()
    {
        while (true)
        {
            if (_virtualCamera == null) { }
            else if (_brain.ActiveVirtualCamera == (ICinemachineCamera)_virtualCamera)
            {
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
}
