using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UnitMenu : MonoBehaviour
{
    [SerializeField, Tooltip("ロボット")]
    RobotBase _robot;

    Button[] _buttons;
    bool _isActive = false;

    [SerializeField, Tooltip("ロボット")]
    Canvas _canvas1;
    [SerializeField, Tooltip("ロボット")]
    Canvas _canvas2;
    [SerializeField, Tooltip("ロボット")]
    Canvas _canvas3;


    // Start is called before the first frame update
    void Start()
    {
        _buttons = transform.GetComponentsInChildren<Button>();
        Close();
    }

    public void Open()
    {
        _canvas1?.gameObject.SetActive(false);
        gameObject.SetActive(true);
        for (int i = 0; i < _buttons.Length; i++)
        {
            _canvas2?.gameObject.SetActive(true);
            if (i < _robot.Mounts.Length)
            {
                _buttons[i].gameObject.SetActive(true);
                _buttons[i].GetComponentInChildren<Text>().text = _robot.Mounts[i].Name;
                var index = i;
                _buttons[i].onClick.AddListener(() =>
                {
                    MountOpen(index);
                });
            }
            else
            {
                _buttons[i].gameObject.SetActive(false);
            }
        }
        _isActive = true;
    }

    public void Close()
    {
        _canvas1?.gameObject?.SetActive(true);
        _canvas3?.gameObject?.SetActive(false);
        _canvas2?.gameObject?.SetActive(false);
        foreach (Button button in _buttons)
        {
            button.GetComponentInChildren<Text>().text = "";
            button.onClick.RemoveAllListeners();
        }
        _isActive = false;
        gameObject.SetActive(false);
    }

    public void Action()
    {
        if (!_isActive)
        {
            Open();
        }
        else
        {
            Close();
        }
    }

    public void MountOpen(int index)
    {
        _canvas2?.gameObject.SetActive(false);
        _canvas3?.gameObject.SetActive(true);
        for (int i = 0; i < Mathf.Min(_buttons.Length); i++)
        {
            if (i < _robot.Mounts[index].SupportedUnits.Length)
            {
                _buttons[i].gameObject.SetActive(true);
                _buttons[i].onClick.RemoveAllListeners();
                var index2 = i;
                _buttons[i].onClick.AddListener(() =>
                {
                    _robot.Mounts[index].Unit = _robot.Mounts[index].SupportedUnits[index2];
                    Close();
                    GameManager.Instance.Pause();

                });
                _buttons[i].GetComponentInChildren<Text>().text = _robot.Mounts[index].SupportedUnits[i].name;
                _buttons[i].gameObject.IsDestroyed();
            }
            else
            {
                _buttons[i].gameObject.SetActive(false);
            }
        }
    }
}
