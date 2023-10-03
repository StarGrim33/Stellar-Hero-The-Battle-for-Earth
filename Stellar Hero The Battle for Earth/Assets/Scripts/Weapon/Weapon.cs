using Assets.Scripts.Components.Checkers;
using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IWeapon
{
    [SerializeField] protected Cooldown _shotCooldown;
    [SerializeField] private CheckCircleOverlap _enemyChecker;
    [SerializeField] private SpriteRenderer _sprite;

    [Space, Header("Bullet")]
    [SerializeField] protected PoolObjectSpawnComponent _spawnComponent;
    [SerializeField] protected float _force = 10;
    [SerializeField] private GameObject _crosshair;

    [Space, Header("Reload and Ammo")]
    [SerializeField] protected int _maxAmmo = 6;
    [SerializeField] protected float _reloadTime = 2f;
    [SerializeField] protected int _currentAmmo;

    protected bool _isReloading = false;

    public Vector3 Target { get; private set; }

    protected List<IDamageable> _enemies;
    protected Vector2 _directionToTarget;
    protected IDamageable _currentTarget;

    private void Start()
    {
        _currentAmmo = _maxAmmo;
        _sprite.flipY = true;
    }

    private void Update()
    {
        if (StateManager.Instance.CurrentGameState == GameStates.Paused)
            return;

        _enemies = _enemyChecker.Check<IDamageable>();

        if (_currentTarget == null)
            DisableCrossHair();

        if (_currentTarget == null || _currentTarget.IsAlive == false)
        {
            _currentTarget = FindClosestLivingEnemy();
        }

        if (_currentTarget != null)
        {
            if (_crosshair.gameObject.activeSelf == false)
                _crosshair.gameObject.SetActive(true);

            RotateToTarget(_currentTarget.TargetTransform.position);
            UpdateCrossHairPosition(_currentTarget.TargetTransform.position);
        }

        if (_currentTarget != null && _shotCooldown.IsReady() && !_isReloading)
        {
            if (_currentAmmo > 0)
            {
                RotateToTarget(_currentTarget.TargetTransform.position);
                _shotCooldown.Reset();
                _currentAmmo--;
                SpawnBullet();
            }
            else
            {
                StartReloading();
            }
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

    public void PerformShot()
    {
        if (!_isReloading && _enemies != null && _enemies.Count > 0 && _shotCooldown.IsReady())
        {

            if (_currentAmmo > 0 && _currentTarget != null)
            {
                RotateToTarget(_currentTarget.TargetTransform.position);
                _shotCooldown.Reset();
                _currentAmmo--;
                SpawnBullet();
            }
            else
            {
                StartReloading();
            }
        }
    }

    private void SpawnBullet()
    {
        GameObject gameObject = _spawnComponent.Spawn();

        if (gameObject.TryGetComponent(out BulletSpawner bullet))
        {
            bullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, -_directionToTarget); 
            bullet.gameObject.SetActive(true);
        }
    }

    private void RotateToTarget(Vector3 target)
    {
        Vector2 directionToTarget = (target - transform.position).normalized;
        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
        _sprite.transform.eulerAngles = new Vector3(0, 0, angle);

        if (directionToTarget.x < 0)
        {
            _sprite.flipX = false;
            _sprite.flipY = true;
        }
        else
        {
            _sprite.flipX = false;
            _sprite.flipY = false;
        }

    }

    private void UpdateCrossHairPosition(Vector3 vector)
    {
        _crosshair.transform.position = new Vector2(vector.x, vector.y);
    }

    private void DisableCrossHair()
    {
        _crosshair.gameObject.SetActive(false);
    }

    private void StartReloading()
    {
        if (!_isReloading && _currentAmmo < _maxAmmo)
        {
            _isReloading = true;
            StartCoroutine(ReloadCoroutine());
        }
    }

    private IEnumerator ReloadCoroutine()
    {
        yield return new WaitForSeconds(_reloadTime);
        _currentAmmo = _maxAmmo;
        _isReloading = false;
    }
}
