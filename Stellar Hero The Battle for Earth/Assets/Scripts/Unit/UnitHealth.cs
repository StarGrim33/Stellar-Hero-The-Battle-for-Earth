using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class UnitHealth : MonoBehaviour
{
    [SerializeField] private Unit _unit;

    protected float _maxHealth;
    protected float ÑurrenHealth;

    protected virtual void OnEnable()
    {
        _maxHealth = _unit.Config.Health;
        ÑurrenHealth = _maxHealth;
    }

    protected virtual void Die() { }
}
