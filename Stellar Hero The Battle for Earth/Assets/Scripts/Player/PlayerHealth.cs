using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerUnit))]
public class PlayerHealth : UnitHealth, IDamageable
{
    public event UnityAction<int> OnHealthChanged;

    public event UnityAction PlayerDead;

    public float MaxHealth { get; private set; }

    public float CurrentHealth
    {
        get
        {
            return _currenHealth;
        }
        private set
        {
            _currenHealth = Mathf.Clamp(value, 0, _maxHealth);

            if (_currenHealth <= 0)
                Die();
        }
    }

    protected override void Die()
    {
        PlayerDead?.Invoke();
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0)
            throw new ArgumentException("Value cannot be negative", nameof(damage));

        CurrentHealth -= damage;
        Debug.Log($"Health is {CurrentHealth}");
    }
}