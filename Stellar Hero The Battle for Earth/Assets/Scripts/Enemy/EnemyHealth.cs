using System;
using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
public class EnemyHealth : UnitHealth, IDamageable
{
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

    public void TakeDamage(int damage)
    {
        if (damage <= 0)
            throw new ArgumentException("Value cannot be negative", nameof(damage));

        CurrentHealth -= damage;
    }

    protected override void Die()
    {
        Destroy(gameObject);
    }
}
