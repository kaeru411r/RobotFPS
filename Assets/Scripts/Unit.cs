using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour, IUnit
{
    Mount _mount;

    /// <summary>
    /// �@�̂Ƀ��j�b�g�𑕔�����
    /// </summary>
    /// <param name="robot"></param>
    public void Attach(RobotBase robot, Mount mount)
    {
    }


    public void Detach()
    {
    }

    public void Pause()
    {
    }

    public void Resume()
    {
    }
}
