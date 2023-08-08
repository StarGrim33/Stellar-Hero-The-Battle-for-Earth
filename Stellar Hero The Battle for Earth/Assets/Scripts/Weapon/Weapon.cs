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

    public Vector3 Target { get; private set; }

    private List<GameObject> _enemies;
    private Vector2 _directionToTarget;

    public void PerformShot()
    {
        _enemies = _enemyChecker.Check();

        if (_enemies != null && _enemies.Count > 0 && _shotCooldown.IsReady())
        {
            if (_enemies.Count > 0)
            {
                Target = _enemies[0].transform.position;

                RotateToTarget(Target);
            }

            _shotCooldown.Reset();

            SpawnBullet();
        }
    }

    private void SpawnBullet()
    {
        GameObject gameObject = _spawnComponent.Spawn();

        if(gameObject.TryGetComponent(out Bullet bullet))
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
}
