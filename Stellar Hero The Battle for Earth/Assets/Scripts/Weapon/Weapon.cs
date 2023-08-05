using Assets.Scripts.Components.Checkers;
using Assets.Scripts.Utils;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private Cooldown _shotCooldown;
    [SerializeField] private CheckCircleOverlap _enemyChecker;
    [SerializeField] private Transform _shootPoint;

    [Space, Header("Bullet")]
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _shootForce = 200f;

    private List<GameObject> _enemies;

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
            PerformShot(_shootPoint);
        }
    }

    private void PerformShot(Transform shootPoint)
    {
        var bullet = Instantiate(_bulletPrefab, transform);
        bullet.transform.position = shootPoint.position;
        var rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(-shootPoint.up * _shootForce, ForceMode2D.Impulse);
    }

    private void RotateToTarget(Vector3 target)
    {
        Vector3 directionToTarget = target - transform.position;
        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg + 90f;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
