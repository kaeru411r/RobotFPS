using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public static GameManager Instance => _instance;

    public List<IPause> Pauses { get => _pauses; set => _pauses = value; }
    public bool IsPause { get => _isPause; set => _isPause = value; }

    static GameManager _instance = new GameManager();
    private GameManager() { }

    List<IPause> _pauses = new List<IPause>();
    bool _isPause = false;



    public void Pause()
    {
        Pause(!_isPause);
    }

    public void Pause(bool isPause)
    {
        if (isPause)
        {
            _isPause = true;
            _pauses.ForEach(p => p.Pause());
        }
        else
        {
            _isPause = false;
            _pauses.ForEach(p => p.Resume());
        }
    }
}
