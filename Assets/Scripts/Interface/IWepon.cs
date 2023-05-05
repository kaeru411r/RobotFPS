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
    
    public void Fire(WeponBase.Phase phase);
    /// <summary>
    /// �Ə��{�^������
    /// </summary>
    /// <param name="phase"></param>
    public void Aim(WeponBase.Phase phase);

    /// <summary>
    /// �����[�h�{�^������
    /// </summary>
    /// <param name="phase"></param>
    public void Reroad(WeponBase.Phase phase);
}
