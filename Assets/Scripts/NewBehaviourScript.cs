using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    Vector3 a;
    [SerializeField]
    Vector3 b;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(1f / 0f);
        Debug.Log(Mathf.Min(float.PositiveInfinity, 1));
    }

    // Update is called once per frame
    void Update()
    {
    }
}
