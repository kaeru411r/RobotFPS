using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ロボットを移動させるコンポーネント
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [SerializeField, Tooltip("基本歩行速度")]
    float _speed;
    [SerializeField, Tooltip("左右速度補正")]
    float _sideCorrection;
    [SerializeField, Tooltip("後方速度補正")]
    float _backCorrection;

    Rigidbody _rb;
    Vector3 _velocity;

    public float Speed { get => _speed; set => _speed = value; }
    public float SideCorrection { get => _sideCorrection; set => _sideCorrection = value; }
    public float BackCorrection { get => _backCorrection; set => _backCorrection = value; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        StartCoroutine(MoveCycle());
    }

    public void Move(Vector2 velocity)
    {
        if (velocity.magnitude > 1)
        {
            velocity = velocity.normalized;
        }

        float x = velocity.x * _sideCorrection * _speed;
        float y = velocity.y * (velocity.y < 0 ? _backCorrection : 1) * _speed;

        _velocity = new Vector3(x, 0, y);
    }


    IEnumerator MoveCycle()
    {
        while (true)
        {
            //_rb.MoveRotation(Quaternion.AngleAxis(_turn, _rb.transform.up));
            if (_velocity.magnitude > 0)
            {
                Vector3 forword = transform.forward;
                Vector3 right = transform.right;
                Vector3 up = transform.up;
                Vector3 velocity = right * _velocity.x + up * _velocity.y + forword * _velocity.z;
                var dot = Vector3.Dot(velocity.normalized, _rb.velocity / velocity.magnitude);
                dot = Mathf.Clamp(dot, 0, 1);
                Vector3 force = velocity * (1f - dot * dot);
                _rb.AddForce(force, ForceMode.Acceleration);
                if (name == "Robot" && force.magnitude != 0)
                {
                    Debug.Log($"{_rb.velocity.magnitude}, {velocity.normalized.magnitude}, {(_rb.velocity / velocity.magnitude).magnitude}, {Vector3.Dot(velocity.normalized, _rb.velocity / velocity.magnitude)}");
                }
                Debug.DrawRay(transform.position, _velocity, Color.red);
                Debug.DrawRay(transform.position, velocity, Color.blue);
                Debug.DrawRay(transform.position, _rb.velocity, Color.yellow);
                Debug.DrawRay(transform.position, force, Color.green);
                Debug.DrawRay(transform.position, forword, Color.white);
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
