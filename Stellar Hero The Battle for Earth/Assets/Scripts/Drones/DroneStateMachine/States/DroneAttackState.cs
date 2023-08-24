using Assets.Scripts.Components.Checkers;
using System.Collections.Generic;
using UnityEngine;

public class DroneAttackState : IStateSwitcher
{
    private CheckCircleOverlap _enemyChecker;
    private ParticleSystemPlayer _shotEffect;
    private DroneStateMachine _machine;
    private List<IDamageable> _enemyList;
    private Transform _transform;
    private IDamageable _currentTarget;

    private int _damage = 15;
    private float _delay = 2f;
    private float _lastAttackTime;

    public DroneAttackState(DroneStateMachine machine, CheckCircleOverlap checker, ParticleSystemPlayer shotEffect, Transform transformDrone)
    {
        _enemyChecker = checker;
        _machine = machine;
        _shotEffect = shotEffect;
        _transform = transformDrone;
    }

    public void Enter()
    {
        _enemyList = new List<IDamageable>();
        Debug.Log("Drone is in attack state");
    }

    public void Exit()
    {
        _machine.SetMovementState();
    }

    public void Update()
    {
        _enemyList = _enemyChecker.Check<IDamageable>();

        if (_currentTarget == null || _currentTarget.IsAlive == false)
            _currentTarget = FindClosestLivingEnemy();

        if (_enemyList.Count < 0)
            return;

        if (_lastAttackTime <= 0 && _currentTarget != null)
        {
            ShootAtEnemy(_currentTarget);
            _lastAttackTime = _delay;
        }

        _lastAttackTime -= Time.deltaTime;
    }

    private void ShootAtEnemy(IDamageable enemy)
    {
        Vector2 direction = (enemy.TargetTransform.position - _enemyChecker.transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(_enemyChecker.transform.position, direction, Mathf.Infinity);

        if (hit.collider != null && hit.collider.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.TakeDamage(_damage);
            _shotEffect.PlayEffect();
        }
    }

    private IDamageable FindClosestLivingEnemy()
    {
        IDamageable closestEnemy = null;
        float closestDistance = float.MaxValue;

        foreach (var enemy in _enemyList)
        {
            if (enemy.IsAlive)
            {
                float distanceToEnemy = Vector3.Distance(_transform.position, enemy.TargetTransform.position);

                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = enemy;
                }
            }
        }

        return closestEnemy;
    }
}
