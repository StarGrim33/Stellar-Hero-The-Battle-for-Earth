using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerUnit))]
public class PlayerHealth : UnitHealth, IDamageable
{
    [SerializeField] private Animator _animator;
     
    private PlayerMovement _playerMovement;

    private bool _isInvulnerable = false;
    private bool _isImmortal = false;
    private int _immortalityTime = 5;
    private float _remainingImmortalityTime;

    public event UnityAction<float, float> OnHealthChanged;

    public event UnityAction PlayerDead;

    public event UnityAction Immortality;

    public float MaxHealth => _maxHealth;

    protected override void OnEnable()
    {
        base.OnEnable();
        _maxHealth = PlayerCharacteristics.I.GetValue(Characteristics.MaxHealth);
    }

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
            return ÑurrenHealth;
        }
        private set
        {
            ÑurrenHealth = Mathf.Clamp(value, 0, _maxHealth);

            if (ÑurrenHealth <= 0)
                Die();
        }
    }

    public Transform TargetTransform => transform;

    public bool IsAlive => ÑurrenHealth > 0;

    protected override void Die()
    {
        _animator.SetTrigger(Constants.DeadState);
        StateManager.Instance.SetState(GameStates.Paused);
        PlayerDead?.Invoke();
    }

    public void HealUp(int value)
    {
        if (value <= 0)
            throw new ArgumentException("Value cannot be negative", nameof(value));

        CurrentHealth += value;
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (_isImmortal)
            return;

        if (damage <= 0)
            throw new ArgumentException("Value cannot be negative", nameof(damage));

        if (_isInvulnerable)
            return;

        CurrentHealth -= damage;
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
        Debug.Log($"Health is {CurrentHealth}");
    }

    public void InvulnerableActivated(bool isInvulnerable) => _isInvulnerable = isInvulnerable;

    public void ActivateImmortal()
    {
        _isImmortal = true;
        Immortality?.Invoke();
        StartCoroutine(ImmortalityTime());
    }

    private IEnumerator ImmortalityTime()
    {
        _remainingImmortalityTime = _immortalityTime;

        while (_remainingImmortalityTime > 0)
        {
            _remainingImmortalityTime -= Time.deltaTime;
            yield return null;
        }

        _isImmortal = false;
    }
}