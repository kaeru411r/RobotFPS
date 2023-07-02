using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DummyTarget : MonoBehaviour
{
    [SerializeField, Tooltip("ƒ_ƒ~[")]
    RobotBase[] _dummys = new RobotBase[0];
    [SerializeField, Tooltip("‚‚³")]
    float _height;
    [SerializeField, Tooltip("‚¸‚ê‚Ì‘å‚«‚³")]
    float _gap;
    private void Start()
    {
        Array.ForEach(_dummys, d =>
        {
            var target = d;
            d.OnDown.Add(() => Respawn(d));
        }
        );
    }

    public void Respawn(RobotBase dummy)
    {
        var gap = new Vector3(UnityEngine.Random.Range(-1, 1), 0, UnityEngine.Random.Range(-1, 1)).normalized * _gap;
        gap.y = _height;
        var robo = Instantiate(dummy, dummy.transform.position + gap, dummy.transform.rotation);
        robo.gameObject.SetActive(true);
        //robo.OnDown.Add(() => Respawn(robo));
    }
}
