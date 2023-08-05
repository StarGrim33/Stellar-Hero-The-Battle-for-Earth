using Assets.Scripts.Utils;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Riffle _riffle;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Cooldown _shootCooldown;

    private float _distance = 5f;
    private Transform _target;
    private List<IWeapon> _weapon;
    private IWeapon _currentWeapon;
    private LayerMask _enemyLayer;
    private float _fireRate = 1f;
    private bool _canShoot = true; // Может ли игрок сейчас стрелять

    private void Awake()
    {
        _enemyLayer = LayerMask.GetMask("Enemy");
    }

    private void Update()
    {
        FindTarget();
    }

    public void PerformAttack()
    {
        _riffle.PerformShot(_shootPoint);
    }

    private void FindTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _distance, _enemyLayer);

        if (colliders.Length > 0)
        {
            var target = colliders[0].transform.position;

            TryShoot();

            Vector3 directionToTarget = target - transform.position;
            float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg + 90f;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }

    private void TryShoot()
    {
        if (_shootCooldown.IsReady())
        {
            _shootCooldown.Reset();
            _riffle.PerformShot(_shootPoint);
        }
    }
}
