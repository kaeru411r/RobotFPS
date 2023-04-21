using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBase : MonoBehaviour
{
    [SerializeField, HideInInspector]
    Mount[] _mounts = new Mount[0];

    public Mount[] Mounts { get => _mounts;}

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
