using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInputSetting : MonoBehaviour
{
    [SerializeField, Tooltip("")]
    RobotBase _robotBase;


    PlayerInput _playerInput;

    PlayerInput PlayerInput
    {
        get
        {
            if (_playerInput == null)
            {
                _playerInput = GetComponent<PlayerInput>();
                _playerInput.notificationBehavior = PlayerNotifications.InvokeUnityEvents;
            }
            return _playerInput;
        }
    }

    private void OnValidate()
    {
        if (_robotBase)
        {
            PlayerInput.actionEvents.Where(a => a.actionName.Contains("Fire")).FirstOrDefault()?
                .AddListener(callback =>
                {
                    var phase = Input2Wepon(callback.phase);
                    if (phase != null)
                    {
                        _robotBase.OnFire(phase.Value);
                    }
                });
            PlayerInput.actionEvents.Where(a => a.actionName.Contains("Aim")).FirstOrDefault()?
                .AddListener(callback =>
                {
                    var phase = Input2Wepon(callback.phase);
                    if (phase != null)
                    {
                        _robotBase.OnAim(phase.Value);
                    }
                });
            PlayerInput.actionEvents.Where(a => a.actionName.Contains("Reload")).FirstOrDefault()?
                .AddListener(callback =>
                {
                    var phase = Input2Wepon(callback.phase);
                    if (phase != null)
                    {
                        _robotBase.OnReload(phase.Value);
                    }
                });
        }
    }


    WeponActionPhase? Input2Wepon(InputActionPhase phase)
    {
        if (phase == InputActionPhase.Waiting && phase == InputActionPhase.Disabled) { return null; }
        WeponActionPhase? result;
        switch (phase)
        {
            case InputActionPhase.Started:
                result = WeponActionPhase.Started;
                break;
            case InputActionPhase.Performed:
                result = WeponActionPhase.Performed;
                break;
            case InputActionPhase.Canceled:
                result = WeponActionPhase.Canceled;
                break;
            default:
                result = null;
                break;
        }

        return result;
    }
}