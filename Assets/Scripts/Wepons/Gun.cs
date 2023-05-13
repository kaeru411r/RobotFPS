using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : WeponBase
{
    [SerializeField, Tooltip("攻撃力")]
    int _atk;
    [SerializeField, Tooltip("発射レート")]
    float _fireRate;
    [SerializeField, Tooltip("リロード時間")]
    float _reloadTime;
    [SerializeField, Tooltip("射撃モード")]
    FireMode _fireMode;
    [SerializeField, Tooltip("銃口")]
    Transform _muzzle;

    bool _isFirering = false;
    bool _isReloading = false;
    bool _isAiming = false;
    float _fireTime = 0;

    public FireMode Firetype { get => _fireMode; set => _fireMode = value; }

    public override void OnAim(WeponActionPhase phase)
    {
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
        if (phase == WeponActionPhase.Started)
        {
            StartCoroutine(Reload());
        }
    }

    private void Start()
    {
        StartCoroutine(Firering());
    }

    IEnumerator Firering()
    {
        while (true)
        {
            if (_isReloading)
            {
                _fireTime = 0;
            }
            else if(_fireTime <= 0)
            { 
                if (_isFirering)
                {
                    _fireTime = 1 / (_fireRate / 60);
                    if(_fireMode == FireMode.SemiAuto)
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
        if(_isReloading) { yield break; }
        Debug.Log("Reloading");
        _isReloading = true;
        float time = _reloadTime;
        while (true)
        {
            yield return null;
            time -= Time.deltaTime;
            if(time < 0)
            {
                _isReloading = false;
                Debug.Log("Reloaded");
                yield break;
            }
        }
    }

    void Fire()
    {
        Debug.Log("Fire");
    }

    public enum FireMode
    {
        FullAuto,
        SemiAuto,
    }
}
