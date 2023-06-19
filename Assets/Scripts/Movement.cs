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
    Vector3 _velocity = Vector3.zero;

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
        StartCoroutine(BrakeCycle());
    }


    public void Move(Vector2 velocity)
    {
        if (velocity.magnitude > 1)
        {
            velocity = velocity.normalized;
        }

        var x = velocity.x * _sideCorrection * _speed;
        var y = velocity.y * (velocity.y < 0 ? _backCorrection : 1) * _speed;

        _velocity = new Vector3(x, 0, y);
    }


    IEnumerator MoveCycle()
    {
        while (true)
        {
            if (_velocity.magnitude > 0)
            {
                var velocity = transform.right * _velocity.x + transform.forward * _velocity.z;
                var dot = Vector3.Dot(velocity.normalized, _rb.velocity / velocity.magnitude);
                dot = Mathf.Clamp(dot, 0, 1);
                var force = velocity * (1f - dot * dot);
                _rb.AddForce(force, ForceMode.Acceleration);
            }
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator BrakeCycle()
    {
        while (true)
        {
            var force = -_rb.velocity;
            if (_velocity.magnitude > 0)
            {
                var velocity = transform.right * _velocity.x + transform.forward * _velocity.z;
                var dot = Vector3.Dot(velocity.normalized, _rb.velocity / velocity.magnitude);
                force -= -velocity * dot;
            }
            _rb.AddForce(force, ForceMode.VelocityChange);
            yield return new WaitForFixedUpdate();
        }
    }
}
