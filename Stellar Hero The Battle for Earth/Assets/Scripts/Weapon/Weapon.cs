using Assets.Scripts.Components.Checkers;
using Assets.Scripts.Utils;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private Cooldown _shotCooldown;
    [SerializeField] private CheckCircleOverlap _enemyChecker;

    [Space, Header("Bullet")]
    [SerializeField] private PoolObjectSpawnComponent _spawnComponent;
    [SerializeField] private float _force = 10;

    private List<GameObject> _enemies;
    private Vector2 _directionToTarget;

    public void TryShoot()
    {
        if (_shotCooldown.IsReady())
        {
            _enemies = _enemyChecker.Check();

            if (_enemies.Count > 0)
            {
                var target = _enemies[0].transform.position;

                RotateToTarget(target);
            }

            _shotCooldown.Reset();

            SpawnBullet();
        }
    }

    private void SpawnBullet()
    {
        var go = _spawnComponent.Spawn();

        if(go.TryGetComponent(out Bullet bullet))
        {
            bullet.AddForce(_directionToTarget, _force);
        }
    }

    private void RotateToTarget(Vector3 target)
    {
        _directionToTarget = target - transform.position;
        float angle = Mathf.Atan2(_directionToTarget.y, _directionToTarget.x) * Mathf.Rad2Deg + 90f;
        transform.rotation = Quaternion.Euler(angle, angle, angle);
    }
}
