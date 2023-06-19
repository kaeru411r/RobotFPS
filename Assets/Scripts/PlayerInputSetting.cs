using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

/// <summary>
/// ÉvÉåÉCÉÑÅ[ëÄçÏÇÃê›íËÇÇ∑ÇÈ
/// </summary>
[RequireComponent(typeof(PlayerInput), typeof(PlayerRobotCamera), typeof(RobotBase))]
public class PlayerInputSetting : MonoBehaviour
{
    [SerializeField]
    UnitMenu _menu;

    RobotBase _robotBase;


    PlayerInput _playerInput;
    PlayerRobotCamera _robotCamera;


    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.notificationBehavior = PlayerNotifications.InvokeUnityEvents;
        _robotCamera = GetComponent<PlayerRobotCamera>();
        _robotBase = GetComponent<RobotBase>();
    }

    private void Start()
    {
        _playerInput.notificationBehavior = PlayerNotifications.InvokeUnityEvents;
        _playerInput.actionEvents.Where(a => a.actionName.Contains("Fire")).FirstOrDefault()?
            .AddListener(callback =>
            {
                var phase = Input2Wepon(callback.phase);
                if (phase != null)
                {
                    _robotBase.OnFire(phase.Value);
                }
            });
        _playerInput.actionEvents.Where(a => a.actionName.Contains("Aim")).FirstOrDefault()?
            .AddListener(callback =>
            {
                var phase = Input2Wepon(callback.phase);
                if (phase != null)
                {
                    _robotBase.OnAim(phase.Value);
                }
            });
        _playerInput.actionEvents.Where(a => a.actionName.Contains("Reload")).FirstOrDefault()?
            .AddListener(callback =>
            {
                var phase = Input2Wepon(callback.phase);
                if (phase != null)
                {
                    _robotBase.OnReload(phase.Value);
                }
            });
        _playerInput.actionEvents.Where(a => a.actionName.Contains("Move")).FirstOrDefault()?
            .AddListener(callback =>
            {
                _robotBase.OnMove(callback.ReadValue<Vector2>());
            });
        _robotCamera.OnTargetSets.Add(_robotBase.OnTargeting);
        _playerInput.actionEvents.Where(a => a.actionName.Contains("Menu")).FirstOrDefault()?
            .AddListener(callback =>
            {
                if(callback.phase == InputActionPhase.Started)
                {
                    GameManager.Instance.Pause();
                    if (_menu)
                    {
                        _menu.Action();
                    }
                }
            });
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