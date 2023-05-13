using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ユニットのベースクラス
/// </summary>
public abstract class UnitBase : MonoBehaviour
{
    /// <summary>
    /// 機体にユニットを装備する
    /// </summary>
    /// <param name="robot"></param>
    public abstract void Attach(RobotBase robot);
    public abstract void Detach();
    public abstract void Pause();
    public abstract void Resume();


    private void OnEnable()
    {
        Resume();
    }
    private void OnDisable()
    {
        Pause();
    }
    private void OnDestroy()
    {
        Detach();
    }
}
