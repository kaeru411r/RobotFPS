using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���{�b�g���ړ�������R���|�[�l���g
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [SerializeField, Tooltip("��{���s���x")]
    float _speed;
    [SerializeField, Tooltip("���񑬓x")]
    float _turnSpeed;
    [SerializeField, Tooltip("���E���x�␳")]
    float _sideCorrection;
    [SerializeField, Tooltip("������x�␳")]
    float _backCorrection;

    Rigidbody _rb;
    Vector3 _velocity;
    float _turn;

    public float Speed { get => _speed; set => _speed = value; }
    public float TurnSpeed { get => _turnSpeed; set => _turnSpeed = value; }
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

    public void Turn(float turn)
    {
        if(turn < -1)
        {
            turn = -1;
        }
        else if(turn > 1)
        {
            turn = 1;
        }

        _turn = turn;
    }

    IEnumerator MoveCycle()
    {
        while (true)
        {
            _rb.AddForce(_velocity);
            _rb.MoveRotation(Quaternion.AngleAxis(_turn, _rb.transform.up));
            yield return new WaitForFixedUpdate();
        }
    }
}
