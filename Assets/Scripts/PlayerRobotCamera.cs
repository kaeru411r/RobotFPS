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
    List<Action<Vector3, TargetingMode>> _onTargetSets = new List<Action<Vector3, TargetingMode>>();

    public List<Action<Vector3, TargetingMode>> OnTargetSets { get => _onTargetSets;}

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
                    _onTargetSets.ForEach(a => a.Invoke(hit.point, TargetingMode.Position));
                }
            }
            yield return null;
        }
    }
}
