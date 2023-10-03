using Assets.Scripts.Components.Checkers;
using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Weapon : MonoBehaviour, IWeapon
{
    [SerializeField] protected Cooldown _shotCooldown;
    [SerializeField] private CheckCircleOverlap _enemyChecker;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField] private BulletParams _params;

    [Space, Header("Bullet")]
    [SerializeField] protected PoolObjectSpawnComponent _spawnComponent;
    [SerializeField] private GameObject _crosshair;

    [Space, Header("Reload and Ammo")]
    [SerializeField] protected int _maxAmmo = 6;
    [SerializeField] protected float _reloadTime = 1f;
    [SerializeField] protected int _currentAmmo;

    protected bool _isReloading = false;

    public Vector3 Target { get; private set; }

    protected List<IDamageable> _enemies;
    protected Vector2 _directionToTarget;
    protected IDamageable _currentTarget;

    public event UnityAction<bool> Reloading;

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

        if (_currentTarget != null && _isReloading == false)
        {
            if (_crosshair.gameObject.activeSelf == false)
                _crosshair.gameObject.SetActive(true);

            UpdateCrossHair();
            RotateWeaponToTarget(_currentTarget.TargetTransform.position);
        }

        if (_currentTarget != null && _shotCooldown.IsReady() && !_isReloading)
        {
            if (_currentAmmo > 0)
            {
                RotateWeaponToTarget(_currentTarget.TargetTransform.position);
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

            _crosshair.gameObject.SetActive(_currentTarget != null);

            UpdateCrossHairPosition(_currentTarget);
        }
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

        if (gameObject.TryGetComponent(out Bullet bullet))
        {
            bullet.gameObject.SetActive(true);
            bullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, -_directionToTarget); 
            bullet.Shot(transform.position, _currentTarget.TargetTransform.position, _params.BulletSpeed, _params.Damage);
        }
    }

    private void RotateWeaponToTarget(Vector3 target)
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

    private void DisableCrossHair()
    {
        _crosshair.gameObject.SetActive(false);
    }

    private void StartReloading()
    {
        if (_isReloading == false && _currentAmmo < _maxAmmo)
        {
            _isReloading = true;
            Reloading?.Invoke(_isReloading);
            StartCoroutine(ReloadCoroutine());
        }
    }

    private IEnumerator ReloadCoroutine()
    {
        var waitForSeconds = new WaitForSeconds(_reloadTime);
        yield return waitForSeconds;
        _currentAmmo = _maxAmmo;
        _isReloading = false;
        Reloading?.Invoke(_isReloading);
    }

    private void UpdateCrossHairPosition(IDamageable target)
    {
        if (target == null)
            return;

        _crosshair.transform.parent = target.TargetTransform;
        _crosshair.transform.localPosition = Vector3.zero;
    }
}
