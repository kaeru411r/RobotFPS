using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnitTestar : MonoBehaviour
{
    [SerializeField]
    Test[] _tests;


    private void OnValidate()
    {
        if (_tests != null)
        {
            foreach (Test test in _tests)
            {
                if (test.Fire)
                {
                    test.Fire = false;
                    test.Event.Invoke();
                }
            }
        }
    }



    [Serializable]
    public class Test
    {
        public bool Fire;
        public UnityEvent Event;
    }
}
