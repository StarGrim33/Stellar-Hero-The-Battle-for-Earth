using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class UnitHealth : MonoBehaviour
{
    [SerializeField] Unit _unit;

    protected float _maxHealth;
    protected float _currenHealth;

    protected void Awake()
    {
        _maxHealth = _unit.Config.Health;
        Debug.Log(_maxHealth);
    }

    protected virtual void Die() { }
}
