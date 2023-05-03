using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RobotBase : MonoBehaviour
{
    [SerializeField]
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

    public void OnFire(InputAction.CallbackContext callback)
    {
        if(callback.phase == InputActionPhase.Started)
        {
            Debug.Log(1);
        }
    }
}
