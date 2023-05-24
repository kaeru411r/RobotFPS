using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackResult
{
    int _damage;
    bool _isKilled;

    public int Damage { get => _damage; }
    public bool IsKilled { get => _isKilled; }

    public AttackResult(int damage, bool isKilled)
    {
        _damage = damage;
        _isKilled = isKilled;
    }
}
