using Assets.Scripts.Components.Checkers;
using System.Collections.Generic;
using UnityEngine;

public class DroneAttackState : IStateSwitcher
{
    public Transform CurrenTarget { get; private set; }

    private CheckCircleOverlap _enemyChecker;
    private DroneParticleSystem _shotEffect;
    private DroneStateMachine _machine;
    private List<IDamageable> _enemyList;
    private Transform _transform;
    private IDamageable _currentTarget;
    private Transform _heroTransform;
    private DroneParameters _parameters;
    private TrailInstantiator _instantiator;
    private float _lastAttackTime;
    private float _angle;

    public DroneAttackState(DroneStateMachine machine, CheckCircleOverlap checker, DroneParticleSystem shotEffect, Transform transformDrone, Transform heroTransform, DroneParameters droneParameters, TrailInstantiator instantiator)
    {
        _enemyChecker = checker;
        _machine = machine;
        _shotEffect = shotEffect;
        _transform = transformDrone;
        _heroTransform = heroTransform;
        _parameters = droneParameters;
        _instantiator = instantiator;
    }

    public void Enter()
    {
        _enemyList = new List<IDamageable>();
    }

    public void Exit()
    {
        _machine.SetMovementState();
    }

    public void Update()
    {
        if (StateManager.Instance.CurrentGameState == GameStates.Paused)
            return;

        Move();
        CheckEnemiesAndAttack();
    }

    private void ShootAtEnemy(IDamageable enemy)
    {
        CurrenTarget = enemy.TargetTransform;
        Vector2 direction = (enemy.TargetTransform.position - _enemyChecker.transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(_enemyChecker.transform.position, direction, Mathf.Infinity);

        if (hit.collider != null && hit.collider.TryGetComponent<IDamageable>(out var damageable))
        {
            if (damageable is not PlayerHealth)
                damageable.TakeDamage(_parameters.Damage);

             _instantiator.TrailInstantiate(hit.point);
            
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

    private void Move()
    {
        _angle += Time.deltaTime;
        float x = _heroTransform.position.x + _parameters.FlyRadius * Mathf.Cos(_angle);
        float y = _heroTransform.position.y + _parameters.FlyRadius * Mathf.Sin(_angle);
        _transform.position = new Vector3(x, y, _transform.position.z);
    }

    private void CheckEnemiesAndAttack()
    {
        _enemyList = _enemyChecker.Check<IDamageable>();

        if (_currentTarget == null || _currentTarget.IsAlive == false)
            _currentTarget = FindClosestLivingEnemy();

        if (_enemyList.Count < 0)
            return;

        if (_lastAttackTime <= 0 && _currentTarget != null)
        {
            ShootAtEnemy(_currentTarget);
            _lastAttackTime = _parameters.Delay;
        }

        _lastAttackTime -= Time.deltaTime;
    }
}
