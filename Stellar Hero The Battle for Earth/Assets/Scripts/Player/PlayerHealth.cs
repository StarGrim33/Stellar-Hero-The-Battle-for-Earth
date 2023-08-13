using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerUnit), typeof(Animator))]
public class PlayerHealth : UnitHealth, IDamageable
{
    [SerializeField] private Animator _animator;
    private PlayerUnit _playerUnit;

    public event UnityAction<float, float> OnHealthChanged;

    public event UnityAction PlayerDead;

    public float MaxHealth => _maxHealth;

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

    private void Start()
    {
        _playerUnit = GetComponent<PlayerUnit>();
    }

    protected override void Die()
    {
        _animator.SetTrigger(Constants.DeadState);
        StateManager.Instance.SetState(GameStates.Paused);
        PlayerDead?.Invoke();
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0)
            throw new ArgumentException("Value cannot be negative", nameof(damage));

        CurrentHealth -= damage;
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
        Debug.Log($"Health is {CurrentHealth}");
    }
}