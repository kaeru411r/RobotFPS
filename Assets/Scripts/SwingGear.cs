using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class SwingGear : UnitBase
{
    [SerializeField, Tooltip("ù‰ñ‘¬“x")]
    float _turnSpeed;

    Rigidbody _rb;
    Vector3 _direction;

    public float TurnSpeed { get => _turnSpeed; set => _turnSpeed = value; }


    protected override void OnAttach()
    {
        _rb = _robot.GetComponent<Rigidbody>();
        _robot.OnTurnFuncs.Add(SetDirection);
        _turnSpeed = _robot.TurnSpeed;
        StartCoroutine(Turn());
    }


    public void SetDirection(Vector3 turn)
    {
        if (_direction != turn)
        {
            _direction = turn;
        }
    }


    IEnumerator Turn()
    {
        while (true)
        {
            var cross = Vector3.Cross(Vector3.up, _direction);
            var dir = Vector3.Cross(cross, transform.up).normalized;
            var angle = Vector3.Angle(transform.forward, dir);
            angle = Mathf.Min(angle, _turnSpeed * Time.fixedDeltaTime) * (Vector3.Dot(transform.right, dir) < 0 ? -1 : 1);
            transform.Rotate(transform.up, angle);
            yield return new WaitForFixedUpdate();
        }
    }



}
