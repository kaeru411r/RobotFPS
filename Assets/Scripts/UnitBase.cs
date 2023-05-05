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
    public abstract void Set(RobotBase robot);
}
