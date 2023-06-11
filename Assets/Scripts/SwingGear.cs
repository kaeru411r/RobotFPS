using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor.Hardware;
using UnityEngine;

public class SwingGear : UnitBase
{
    [SerializeField, Tooltip("ù‰ñ‘¬“x")]
    float _turnSpeed;

    Rigidbody _rb;
    Vector3 _direction;

    public float TurnSpeed { get => _turnSpeed; set => _turnSpeed = value; }


    public override void Attach(RobotBase robot)
    {
        _rb = robot.GetComponent<Rigidbody>();
        robot.OnTurnFuncs.Add(SetDirection);
        _turnSpeed = robot.TurnSpeed;
        StartCoroutine(Turn());
    }

    public override void Detach()
    {
        //throw new System.NotImplementedException();
    }

    public override void Pause()
    {
        //throw new System.NotImplementedException();
    }

    public override void Resume()
    {
        
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
            StringBuilder sb = new StringBuilder();
            Vector3 cross = Vector3.Cross(Vector3.up, _direction);
            Vector3 dir = Vector3.Cross(cross, transform.up).normalized;
            float angle = Vector3.Angle(transform.forward, dir);
            sb.Append(angle);
            Debug.DrawRay(transform.position, dir * 10);
            angle = Mathf.Min(angle, _turnSpeed * Time.fixedDeltaTime) * (Vector3.Dot(transform.right, dir) < 0 ? -1 : 1);
            sb.Append(", " + angle);
            Debug.Log(sb);
            transform.Rotate(transform.up, angle);
            yield return new WaitForFixedUpdate();
        }
    }



}
