using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UnitMenu : MonoBehaviour
{
    [SerializeField, Tooltip("ÉçÉ{ÉbÉg")]
    RobotBase _robot;

    Button[] _buttons;
    bool _isActive = false;


    // Start is called before the first frame update
    void Start()
    {
        _buttons = transform.GetComponentsInChildren<Button>();
        Close();
    }

    public void Open()
    {
        gameObject.SetActive(true);
        for (int i = 0; i < _buttons.Length; i++)
        {
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
            }
            else
            {
                _buttons[i].gameObject.SetActive(false);
            }
        }
    }
}
