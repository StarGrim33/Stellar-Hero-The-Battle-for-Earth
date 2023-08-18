using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyUnit))]
public class EnemyHealth : UnitHealth, IDamageable
{
    public event UnityAction<EnemyHealth> Dying;

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

    public Transform TargetTransform => transform;

    public bool IsAlive => _currenHealth > 0;

    public void TakeDamage(int damage)
    {
        if (damage <= 0)
            throw new ArgumentException("Value cannot be negative", nameof(damage));

        CurrentHealth -= damage;
    }

    protected override void Die()
    {
        Dying?.Invoke(this);
        gameObject.SetActive(false);
    }
}
