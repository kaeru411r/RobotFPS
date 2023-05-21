using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : WeponBase
{
    [SerializeField, Tooltip("�U����")]
    int _atk;
    [SerializeField, Tooltip("�e��")]
    float _speed;
    [SerializeField, Tooltip("���˃��[�g")]
    float _fireRate;
    [SerializeField, Tooltip("�����[�h����")]
    float _reloadTime;
    [SerializeField, Tooltip("�ˌ����[�h")]
    FireMode _fireMode;
    [SerializeField, Tooltip("�e")]
    Bullet _bullet;
    [SerializeField, Tooltip("�e��")]
    Transform _muzzle;

    bool _isFirering = false;
    bool _isReloading = false;
    bool _isAiming = false;
    bool _isPause = false;
    float _fireTime = 0;
    Rigidbody _rb;

    public FireMode Firetype { get => _fireMode; set => _fireMode = value; }


    public override void Pause()
    {
        _isPause = true;
    }

    public override void Resume()
    {
        _isPause = false;
    }

    public override void OnAim(WeponActionPhase phase)
    {
        if (_isPause) { return; }
        if (phase == WeponActionPhase.Started)
        {
            _isAiming = true;
        }
        else if (phase == WeponActionPhase.Canceled)
        {
            _isAiming = false;
        }
    }

    public override void OnFire(WeponActionPhase phase)
    {
        if (_isPause) { return; }
        if (phase == WeponActionPhase.Started)
        {
            _isFirering = true;
        }
        else if (phase == WeponActionPhase.Canceled)
        {
            _isFirering = false;
        }
    }

    public override void OnReload(WeponActionPhase phase)
    {
        if (_isPause) { return; }
        if (phase == WeponActionPhase.Started)
        {
            StartCoroutine(Reload());
        }
    }

    public override void Attach(RobotBase robot)
    {
        base.Attach(robot);
        _rb = robot.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        StartCoroutine(Firering());
    }

    IEnumerator Firering()
    {
        while (true)
        {
            if (_isPause) { }
            else if (_isReloading)
            {
                _fireTime = 0;
            }
            else if (_fireTime <= 0)
            {
                if (_isFirering)
                {
                    _fireTime = 1 / (_fireRate / 60);
                    if (_fireMode == FireMode.SemiAuto)
                    {
                        _isFirering = false;
                    }
                    Fire();
                }
            }
            else
            {
                _fireTime -= Time.deltaTime;
            }
            yield return null;
        }
    }

    IEnumerator Reload()
    {
        if (_isReloading) { yield break; }
        Debug.Log("Reloading");
        _isReloading = true;
        float time = _reloadTime;
        while (true)
        {
            yield return null;
            if (_isPause) { continue; }
            time -= Time.deltaTime;
            if (time < 0)
            {
                _isReloading = false;
                Debug.Log("Reloaded");
                yield break;
            }
        }
    }

    void Fire()
    {
        if (_bullet && _muzzle)
        {
            var bullet = Instantiate(_bullet, _muzzle.position, _muzzle.rotation);
            if (_rb)
            {
                bullet.RigidBody.velocity = _rb.velocity;
            }
            bullet.Fire(_atk, _speed, 10);
            Debug.Log("Fire");
        }
    }

    public enum FireMode
    {
        FullAuto,
        SemiAuto,
    }
}
