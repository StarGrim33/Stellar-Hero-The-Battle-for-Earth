using Assets.Scripts.Components.Checkers;
using Assets.Scripts.Utils;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Weapon : MonoBehaviour, IWeapon
{
    [SerializeField] protected Cooldown _shotCooldown;
    [SerializeField] protected SpriteRenderer _weaponSprite;
    [SerializeField] protected GameObject _crosshair;
    [SerializeField] private CheckCircleOverlap _enemyChecker;

    [Space, Header("Reload and Ammo")]
    protected int _maxAmmo = 6;
    [SerializeField] protected float _reloadTime = 1f;
    protected int _currentAmmo = 6;
    [SerializeField] private PlayerMovement _movement;

    protected bool _isReloading = false;

    protected List<IDamageable> _enemies;
    protected IDamageable _currentTarget;

    public Vector3 Target { get; private set; }

    public int CurrentAmmo => _currentAmmo;

    public int MaxAmmo => _maxAmmo;

    public event UnityAction<int, int> AmmoChanged;

    private void Start()
    {
        _weaponSprite.flipY = true;
    }

    private void Update()
    {
        if (StateManager.Instance.CurrentGameState == GameStates.Paused)
            return;

        HandleWeaponUpdate();
    }

    public void PerformShot()
    {
        if (_isReloading == false && _enemies != null && _enemies.Count > 0 && _shotCooldown.IsReady())
        {
            if (_currentAmmo > 0 && _currentTarget != null)
            {
                RotateWeaponToTarget(_currentTarget.TargetTransform.position);
                _shotCooldown.Reset();
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
            DisableCrossHair();

        if (_currentTarget == null || _currentTarget.IsAlive == false)
        {
            _currentTarget = FindClosestLivingEnemy();


            Vector2 playerDirection = _movement.Direction;

            if (playerDirection.x < 0)
            {
                _weaponSprite.flipX = true;
            }
            else
            {
                _weaponSprite.flipX = false;
            }
        }

        if (_currentTarget != null && _isReloading == false)
        {
            if (_crosshair.activeSelf == false)
                _crosshair.SetActive(true);

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
            if (_currentTarget.IsAlive == false)
                _currentTarget = FindClosestLivingEnemy();

            _crosshair.SetActive(_currentTarget != null);

            UpdateCrossHairPosition(_currentTarget);
        }
    }

    protected abstract void RotateWeaponToTarget(Vector3 target);

    protected abstract void DisableCrossHair();

    protected abstract void StartReloading();

    protected abstract void UpdateCrossHairPosition(IDamageable target);

    protected abstract void SpawnBullet();
}
