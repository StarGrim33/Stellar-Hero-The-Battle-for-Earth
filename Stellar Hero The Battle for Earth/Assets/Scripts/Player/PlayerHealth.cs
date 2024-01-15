using System;
using System.Collections;
using Utils;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerHealth : UnitHealth, IDamageable
    {
        private readonly int _immortalityTime = 5;

        [SerializeField] private Animator _animator;
        [SerializeField] private HandSpriteHandler _handSpriteHandler;
        private PlayerMovement _playerMovement;
        private BaseParticleSystem _effects;
        private bool _isInvulnerable;
        private bool _isImmortal;
        private float _remainingImmortalityTime;

        public event Action<float, float> OnHealthChanged;

        public event Action PlayerDead;

        public event Action Immortality;

        public override float CurrentHealth
        {
            get
            {
                return base.CurrentHealth;
            }
            protected set
            {
                base.CurrentHealth = Mathf.Clamp(value, 0, base.MaxHealth);

                if (base.CurrentHealth <= 0)
                    Die();
            }
        }

        public Transform TargetTransform => transform;

        public bool IsAlive => base.CurrentHealth > 0;

        private void Awake()
        {
            Init();
        }

        private void OnDisable()
        {
            _playerMovement.Dashing -= InvulnerableActivated;
        }

        public void Heal(int healingAmount)
        {
            if (healingAmount <= 0)
                throw new ArgumentException(nameof(healingAmount));

            CurrentHealth += healingAmount;
            OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
        }

        public void TakeDamage(int damage)
        {
            if (_isImmortal)
                return;

            if (damage <= 0)
                throw new ArgumentException(nameof(damage));

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

        public void InvulnerableActivated(bool isInvulnerable)
        {
            _isInvulnerable = isInvulnerable;
        }

        public void ActivateImmortal()
        {
            _effects.PlayEffect();
            _isImmortal = true;
            Immortality?.Invoke();
            StartCoroutine(ImmortalityTime());
        }

        protected override void Die()
        {
            _handSpriteHandler.Disable();
            _animator.SetTrigger(Constants.DeadState);
            StateManager.Instance.SetState(GameStates.Paused);
            PlayerDead?.Invoke();
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

        private void Init()
        {
            _effects = GetComponent<BaseParticleSystem>();
            _playerMovement = GetComponent<PlayerMovement>();
            _playerMovement.Dashing += InvulnerableActivated;
            SetMaxHealth();
            PlayerCharacteristics.I.CharacteristicChanged += SetMaxHealth;
        }
    }
}