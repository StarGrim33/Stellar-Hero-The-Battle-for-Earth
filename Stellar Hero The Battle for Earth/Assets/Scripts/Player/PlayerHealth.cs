using System;
using System.Collections;
using UnityEngine;
using Utils;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerHealth : UnitHealth, IDamageable
{
    [SerializeField] private Animator _animator;
    [SerializeField] private HandSpriteHandler _handSpriteHandler;
    private PlayerMovement _playerMovement;
    private BaseParticleSystemPlayer _effects;
    private bool _isInvulnerable = false;
    private bool _isImmortal = false;
    private int _immortalityTime = 5;
    private float _remainingImmortalityTime;

    public event Action<float, float> OnHealthChanged;
    public event Action PlayerDead;
    public event Action Immortality;

    public new float MaxHealth => base.MaxHealth;

    public float CurrentHealth
    {
        get
        {
            return ÑurrenHealth;
        }
        private set
        {
            ÑurrenHealth = Mathf.Clamp(value, 0, base.MaxHealth);

            if (ÑurrenHealth <= 0)
                Die();
        }
    }

    public Transform TargetTransform => transform;

    public bool IsAlive => ÑurrenHealth > 0;

    protected override void OnEnable() => base.OnEnable();

    private void Awake() => _effects = GetComponent<BaseParticleSystemPlayer>();

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerMovement.Dashing += InvulnerableActivated;
        SetMaxHealth();
        PlayerCharacteristics.I.CharacteristicChanged += SetMaxHealth;
    }

    private void OnDisable() => _playerMovement.Dashing -= InvulnerableActivated;

    protected override void Die()
    {
        _handSpriteHandler.Disable();
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
    }

    public void Revive()
    {
        CurrentHealth = MaxHealth;
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
        _animator.SetBool(Constants.IsRevive, true);
        _handSpriteHandler.Enable();
    }

    public void InvulnerableActivated(bool isInvulnerable) => _isInvulnerable = isInvulnerable;

    public void ActivateImmortal()
    {
        _effects.PlayEffect(ParticleEffects.Shield);
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

    private void SetMaxHealth()
    {
        base.MaxHealth = PlayerCharacteristics.I.GetValue(Characteristics.MaxHealth);
        CurrentHealth = MaxHealth;
    }
}