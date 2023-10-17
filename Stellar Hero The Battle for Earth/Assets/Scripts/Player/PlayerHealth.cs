using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerUnit))]
public class PlayerHealth : UnitHealth, IDamageable
{
    [SerializeField] private Animator _animator;

    private PlayerMovement _playerMovement;

    private bool _isInvulnerable = false;

    public event UnityAction<float, float> OnHealthChanged;

    public event UnityAction PlayerDead;

    public float MaxHealth => _maxHealth;


    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerMovement.Dashing += InvulnerableActivated;
    }

    private void OnDisable()
    {
        _playerMovement.Dashing -= InvulnerableActivated;
    }

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

        if (_isInvulnerable)
            return;

        CurrentHealth -= damage;
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
        Debug.Log($"Health is {CurrentHealth}");
    }

    public void InvulnerableActivated(bool isInvulnerable) => _isInvulnerable = isInvulnerable;
}