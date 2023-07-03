using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class SwingGear : UnitBase
{
    [SerializeField, Tooltip("ù‰ñ‘¬“x")]
    float _turnSpeed;

    Vector3 _direction;
    Coroutine _turnCoroutine;

    public float TurnSpeed { get => _turnSpeed; set => _turnSpeed = value; }


    protected override void OnAttach()
    {
        base.OnAttach();
        _robot.OnTurnFuncs.Add(SetDirection);
        _turnSpeed = _robot.TurnSpeed;
        _turnCoroutine = _mono.StartCoroutine(Turn());
    }

    protected override void OnDetach()
    {
        _mono.StopCoroutine(_turnCoroutine);
        base.OnDetach();
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
            while (_isPause)
            {
                yield return null;
            }
            var cross = Vector3.Cross(Vector3.up, _direction);
            var dir = Vector3.Cross(cross, Vector3.up).normalized;
            var angle = Vector3.Angle(_mono.transform.forward, dir);
            angle = Mathf.Min(angle, _turnSpeed * Time.fixedDeltaTime) * (Vector3.Dot(_mono.transform.right, dir) < 0 ? -1 : 1);
            _mono.transform.Rotate(Vector3.up, angle);
            yield return new WaitForFixedUpdate();
        }
    }



}
