using Assets.Scripts.Components.Checkers;
using Assets.Scripts.Utils;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IWeapon
{
    [SerializeField] private int _damage;
    [SerializeField] private Cooldown _shotCooldown;
    [SerializeField] private CheckCircleOverlap _enemyChecker;

    [Space, Header("Bullet")]
    [SerializeField] private PoolObjectSpawnComponent _spawnComponent;
    [SerializeField] private float _force = 10;
    [SerializeField] private GameObject _crosshair;

    public Vector3 Target { get; private set; }

    private List<GameObject> _enemies;
    private Vector2 _directionToTarget;
    private GameObject _currentTarget;

    private void Update()
    {
        _enemies = _enemyChecker.Check();

        if(_currentTarget == null)
            DisableCrossHair();

        if (_currentTarget == null || _currentTarget.GetComponent<EnemyHealth>().CurrentHealth <= 0)
        {
            _currentTarget = FindClosestLivingEnemy();
        }

        if (_currentTarget != null)
        {
            if(!_crosshair.gameObject.activeSelf)
                _crosshair.gameObject.SetActive(true);

            UpdateCrossHairPosition(_currentTarget.transform.position);
        }

        if (_currentTarget != null && _shotCooldown.IsReady())
        {
            RotateToTarget(_currentTarget.transform.position);
            _shotCooldown.Reset();
            SpawnBullet();
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
        if (_enemies != null && _enemies.Count > 0 && _shotCooldown.IsReady())
        {

            if (_enemies.Count > 0)
            {
                RotateToTarget(_currentTarget.transform.position);
            }

            _shotCooldown.Reset();

            SpawnBullet();
        }
    }

    private void SpawnBullet()
    {
        GameObject gameObject = _spawnComponent.Spawn();

        if (gameObject.TryGetComponent(out Bullet bullet))
        {
            bullet.gameObject.SetActive(true);
            bullet.AddForce(_directionToTarget, _force);
        }
    }

    private void RotateToTarget(Vector3 target)
    {
        _directionToTarget = target - transform.position;
        float angle = Mathf.Atan2(_directionToTarget.y, _directionToTarget.x) * Mathf.Rad2Deg + 90f;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void UpdateCrossHairPosition(Vector3 vector)
    {
        _crosshair.transform.position = new Vector2(vector.x, vector.y);
        Debug.Log($"Crosshair position: {_crosshair.transform.position}, Target position: {vector}");
    }

    private void DisableCrossHair()
    {
        _crosshair.gameObject.SetActive(false);
    }

    private void EnableCrossHair()
    {
        _crosshair.gameObject.SetActive(true);
    }
}
