using System;
using System.Collections.Generic;
using Assets.Scripts.Components.Checkers;
using Assets.Scripts.Utils;
using Player;
using UnityEngine;
using Utils;

namespace Weapon
{
    public abstract class BaseWeapon : MonoBehaviour, IWeapon
    {
        protected int _maxAmmo = 6;
        protected int _currentAmmo = 6;
        protected bool _isReloading;
        protected List<IDamageable> _enemies;
        protected IDamageable _currentTarget;

        [SerializeField] protected Cooldown ShotCooldown;
        [SerializeField] protected SpriteRenderer WeaponSprite;
        [SerializeField] protected float _reloadTime = 1f;
        [SerializeField] protected CrossHair Crosshair;

        [SerializeField] private CheckCircleOverlap _enemyChecker;
        [SerializeField] private PlayerMovement _movement;

        public event Action<int, int> AmmoChanged;

        public int CurrentAmmo => _currentAmmo;

        public int MaxAmmo => _maxAmmo;

        private void Start()
        {
            WeaponSprite.flipY = true;
        }

        private void Update()
        {
            if (StateManager.Instance.CurrentGameState == GameStates.Paused)
                return;

            HandleWeaponUpdate();
        }

        public void PerformShot()
        {
            if (_isReloading == false && _enemies != null && _enemies.Count > 0 && ShotCooldown.IsReady())
            {
                if (_currentAmmo > 0 && _currentTarget != null)
                {
                    RotateWeaponToTarget(_currentTarget.TargetTransform.position);
                    ShotCooldown.Reset();
                    _currentAmmo--;
                    AmmoChanged?.Invoke(_currentAmmo, _maxAmmo);
                    SpawnBullet();
                }
                else
                {
                    StartReloading();
                }
            }
        }

        protected void HandleWeaponUpdate()
        {
            _enemies = _enemyChecker.Check<IDamageable>();

            if (_currentTarget == null)
            {
                DisableCrossHair();
            }

            if (_currentTarget == null || _currentTarget.IsAlive == false)
            {
                _currentTarget = FindClosestLivingEnemy();
                Vector2 playerDirection = _movement.Direction;
                WeaponSprite.flipX = playerDirection.x < 0;
            }

            if (_currentTarget != null && _isReloading == false)
            {
                if (!Crosshair.gameObject.activeSelf)
                {
                    Crosshair.gameObject.SetActive(true);
                }

                UpdateCrossHair();
                RotateWeaponToTarget(_currentTarget.TargetTransform.position);
            }
        }

        private IDamageable FindClosestLivingEnemy()
        {
            IDamageable closestEnemy = null;
            float closestDistance = float.MaxValue;

            foreach (var enemy in _enemies)
            {
                if (enemy.IsAlive)
                {
                    float distanceToEnemy = Vector3.Distance(transform.position, enemy.TargetTransform.position);

                    if (distanceToEnemy < closestDistance)
                    {
                        closestDistance = distanceToEnemy;
                        closestEnemy = enemy;
                    }
                }
            }

            return closestEnemy;
        }

        private void UpdateCrossHair()
        {
            if (_currentTarget == null)
            {
                DisableCrossHair();
                _currentTarget = FindClosestLivingEnemy();
            }
            else
            {
                if (!_currentTarget.IsAlive)
                {
                    _currentTarget = FindClosestLivingEnemy();
                }

                Crosshair.gameObject.SetActive(_currentTarget != null);
                UpdateCrossHairPosition(_currentTarget);
            }
        }

        protected abstract void RotateWeaponToTarget(Vector3 target);

        protected abstract void DisableCrossHair();

        protected abstract void StartReloading();

        protected abstract void UpdateCrossHairPosition(IDamageable target);

        protected abstract void SpawnBullet();
    }
}