using System;
using Assets.Scripts.Components.Checkers;
using Assets.Scripts.Utils;
using Core;
using Utils;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D), typeof(PlayerParticleSystem))]
    public class PlayerMovement : MonoBehaviour, IControllable
    {
        [SerializeField] private float _duration;
        [SerializeField] private float _distance;
        [SerializeField] private Cooldown _dashCooldown;
        [SerializeField] private CheckCircleOverlap _obstacleChecker;
        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private ButtonFiller _dushFiller;
        [SerializeField] private EnterTriggerDamage _enterTriggerDamage;
        private PlayerParticleSystem _particleSystem;
        private Rigidbody2D _rigidBody;
        private Vector2 _direction;
        private Vector2 _startDashPostioin;
        private Vector2 _endDashPostioin;
        private float _dashTimer;
        private bool _isDash;
        private float _speed;

        public event Action<bool> Dashing;

        public Vector3 Direction => _direction;

        public float CurrentSpeed { get; private set; }

        private void Awake()
        {
            _particleSystem = GetComponent<PlayerParticleSystem>();
            _rigidBody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            SetMovementCharacteristic();
            PlayerCharacteristics.I.CharacteristicChanged += SetMovementCharacteristic;
        }

        private void FixedUpdate()
        {
            if (StateManager.Instance.CurrentGameState == GameStates.Paused)
                return;

            if (_isDash)
                Dash();
            else
                Move();
        }

        public void SetDirection(Vector2 direction)
        {
            _sprite.flipX = direction.x < 0;
            _direction = direction;
        }

        public void Move()
        {
            _rigidBody.velocity = _direction * _speed;
            _particleSystem.PlayEffect();
            CurrentSpeed = _rigidBody.velocity.magnitude;
        }

        public void TryDash()
        {
            if (_dashCooldown.IsReady())
            {
                _dashCooldown.Reset();
                _startDashPostioin = transform.position;
                var directionDash = _direction == Vector2.zero ? Vector2.down : _direction;
                _endDashPostioin = _startDashPostioin + directionDash * _distance;
                _dashTimer = 0;
                _isDash = true;
                Dashing?.Invoke(true);
            }
        }

        private void Dash()
        {
            _dushFiller?.StartFilled(_dashCooldown.Value);
            _dashTimer += Time.fixedDeltaTime;
            float time = _dashTimer / _duration;
            transform.position = Vector2.Lerp(_startDashPostioin, _endDashPostioin, time);

            _enterTriggerDamage.OnTriggerDamage();

            if (_obstacleChecker.CheckCount() > 0 || time > 1)
            {
                _isDash = false;
                Dashing?.Invoke(false);
                _enterTriggerDamage.OffTriggerDamage();
            }
        }

        private void SetMovementCharacteristic()
        {
            _speed = PlayerCharacteristics.I.GetValue(Characteristics.Speed);
            _dashCooldown.ChangeCooldownValue(PlayerCharacteristics.I.GetValue(Characteristics.DushCooldown));
            _enterTriggerDamage.SetDamage((int)PlayerCharacteristics.I.GetValue(Characteristics.PlayerTriggerDamage));
        }
    }
}