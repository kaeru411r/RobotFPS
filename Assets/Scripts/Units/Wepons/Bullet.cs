using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// íe
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    const int _enemyLayer = 8;
    const int _bulletLayer = 9;

    int _atk = 0;
    float _speed = 0;
    Rigidbody _rb;
    bool _isFire;

    /// <summary>çUåÇóÕ</summary>
    public int Atk { get => _atk; set => _atk = value; }
    /// <summary>íeë¨</summary>
    public float Speed { get => _speed; set => _speed = value; }
    public Rigidbody RigidBody { get => _rb; set => _rb = value; }

    private void Awake()
    {
        gameObject.layer = _bulletLayer;
        _rb = GetComponent<Rigidbody>();
        _rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    /// <summary>
    /// î≠éÀä÷êî
    /// </summary>
    /// <param name="atk"></param>
    /// <param name="speed"></param>
    public void Fire(int atk, float speed, float lifeTime)
    {
        _atk = atk;
        _speed = speed;
        _rb.velocity = transform.forward * speed;
        _isFire = true;
        Destroy(gameObject, lifeTime);
    }

    private void Hit(Collision hit)
    {
        if (hit.gameObject.layer == _enemyLayer)
        {
            Debug.Log("Hit");
            var result = hit.gameObject.GetComponent<RobotBase>()?.OnDamage(_atk);
            Debug.Log(result.Damage);
        }

        Destroy(gameObject);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (_isFire && collision.gameObject.layer != _bulletLayer)
        {
            Hit(collision);
        }
    }
}
