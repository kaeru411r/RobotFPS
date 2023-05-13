using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器のインターフェース
/// </summary>
public interface IWepon
{
    /// <summary>
    /// 射撃ボタン操作
    /// </summary>
    /// <param name="phase"></param>

    public void OnFire(WeponActionPhase phase);
    /// <summary>
    /// 照準ボタン操作
    /// </summary>
    /// <param name="phase"></param>
    public void OnAim(WeponActionPhase phase);

    /// <summary>
    /// リロードボタン操作
    /// </summary>
    /// <param name="phase"></param>
    public void OnReload(WeponActionPhase phase);
}

