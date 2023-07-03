using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ロボットを移動させるコンポーネント
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class Movement : UnitBase
{
    [SerializeField, Tooltip("基本歩行速度")]
    float _speed;
    [SerializeField, Tooltip("左右速度補正")]
    float _sideCorrection;
    [SerializeField, Tooltip("後方速度補正")]
    float _backCorrection;

    Rigidbody _rb;
    Vector3 _velocity = Vector3.zero;
    Coroutine _moveCoroutine;
    Coroutine _brakeCoroutine;

    public float Speed { get => _speed; set => _speed = value; }
    public float SideCorrection { get => _sideCorrection; set => _sideCorrection = value; }
    public float BackCorrection { get => _backCorrection; set => _backCorrection = value; }


    protected override void OnAttach()
    {
        base.OnAttach();
        _rb = _mono.GetComponent<Rigidbody>();
        _moveCoroutine = _mono.StartCoroutine(MoveCycle());
        _brakeCoroutine = _mono.StartCoroutine(BrakeCycle());
    }

    protected override void OnDetach()
    {
        _mono.StopCoroutine(_moveCoroutine);
        _mono.StopCoroutine(_brakeCoroutine);
        base.OnDetach();
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
                var velocity = _mono.transform.right * _velocity.x + _mono.transform.forward * _velocity.z;
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
            if (_isPause)
            {
                yield return Sleeping();
            }
            var velocity = _rb.velocity;
            velocity.y = 0;
            var force = -velocity;
            if (_velocity.magnitude > 0)
            {
                var moveVelocity = _mono.transform.right * _velocity.x + _mono.transform.forward * _velocity.z;
                var dot = Vector3.Dot(moveVelocity.normalized, velocity / moveVelocity.magnitude);
                force -= -moveVelocity * dot;
                Debug.Log(force);
            }
            _rb.AddForce(force, ForceMode.VelocityChange);
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator Sleeping()
    {
        var buf = _rb.velocity;
        _rb.Sleep();
        _rb.isKinematic = true;
        while (_isPause)
        {
            yield return null;
        }
        _rb.isKinematic = false;
        _rb.WakeUp();
        _rb.velocity = buf;
    }
}
