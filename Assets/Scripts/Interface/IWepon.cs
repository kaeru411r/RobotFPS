using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����̃C���^�[�t�F�[�X
/// </summary>
public interface IWepon
{
    /// <summary>
    /// �ˌ��{�^������
    /// </summary>
    /// <param name="phase"></param>
    public void OnFire(WeponActionPhase phase);

    /// <summary>
    /// �Ə��{�^������
    /// </summary>
    /// <param name="phase"></param>
    public void OnAim(WeponActionPhase phase);

    /// <summary>
    /// �����[�h�{�^������
    /// </summary>
    /// <param name="phase"></param>
    public void OnReload(WeponActionPhase phase);

    /// <summary>
    /// �Ə����߂�
    /// </summary>
    /// <param name="target"></param>
    /// <param name="targetingMode"></param>
    public void OnTargeting(TargetingData data);
}

