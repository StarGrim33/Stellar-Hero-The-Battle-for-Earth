using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class UnitHealth : MonoBehaviour
{
    [SerializeField] private Unit _unit;

    protected float _maxHealth;
    protected float _currenHealth;

    protected void OnEnable()
    {
        _maxHealth = _unit.Config.Health;
        _currenHealth = _maxHealth;
    }

    protected virtual void Die() { }
}
