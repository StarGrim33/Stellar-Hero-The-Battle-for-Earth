using Assets.Scripts.Components.Checkers;
using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class Weapon : MonoBehaviour, IWeapon
{
    [SerializeField] protected int _damage;
    [SerializeField] protected Cooldown _shotCooldown;
    [SerializeField] private CheckCircleOverlap _enemyChecker;

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

    protected List<GameObject> _enemies;
    protected Vector2 _directionToTarget;
    protected GameObject _currentTarget;

    private void Start()
    {
        _currentAmmo = _maxAmmo;
    }

    private void Update()
    {
        if (StateManager.Instance.CurrentGameState == GameStates.Paused)
            return;

        _enemies = _enemyChecker.Check();

        if (_currentTarget == null)
            DisableCrossHair();

        if (_currentTarget == null || _currentTarget.GetComponent<EnemyHealth>().CurrentHealth <= 0)
        {
            _currentTarget = FindClosestLivingEnemy();
        }

        if (_currentTarget != null)
        {
            if (!_crosshair.gameObject.activeSelf)
                _crosshair.gameObject.SetActive(true);

            RotateToTarget(_currentTarget.transform.position);
            UpdateCrossHairPosition(_currentTarget.transform.position);
        }

        if (_currentTarget != null && _shotCooldown.IsReady() && !_isReloading)
        {
            if (_currentAmmo > 0)
            {
                RotateToTarget(_currentTarget.transform.position);
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

    private GameObject FindClosestLivingEnemy()
    {
        GameObject closestEnemy = null;
        float closestDistance = float.MaxValue;

        foreach (var enemy in _enemies)
        {
            if (enemy.GetComponent<EnemyHealth>().CurrentHealth > 0)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

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

            if (_currentAmmo > 0)
            {
                RotateToTarget(_currentTarget.transform.position);
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
            //bullet.AddForce(_directionToTarget, _force);
        }
    }

    private void RotateToTarget(Vector3 target)
    {
        _directionToTarget = (target - transform.position).normalized;
        float angle = Mathf.Atan2(_directionToTarget.y, _directionToTarget.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, -angle);
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
        Debug.Log("ok");
    }
}
