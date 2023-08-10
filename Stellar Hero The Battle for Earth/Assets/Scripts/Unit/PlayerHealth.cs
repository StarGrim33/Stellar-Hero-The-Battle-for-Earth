using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    private float _maxHealth;
    private float _currenHealth;
    private PlayerUnit _unit;

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

    private void Awake()
    {
        _unit = GetComponent<PlayerUnit>();
        MaxHealth = _unit.Config.Health;
        Debug.Log(MaxHealth);
    }

    private void Die()
    {
        PlayerDead?.Invoke();
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0)
            throw new ArgumentException("Value cannot be negative", nameof(damage));

        CurrentHealth -= damage;
    }
}
