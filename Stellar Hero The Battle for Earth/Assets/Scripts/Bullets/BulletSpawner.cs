using Assets.Scripts.Components.Checkers;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private Bullet _template;
    [SerializeField] private CheckCircleOverlap _enemyChecker;
    [SerializeField] private CrossHair _crosshair;
    [SerializeField] private AudioSource _audioSource;

    private List<IDamageable> _enemies;
    private IDamageable _currentTarget;

    private void Update()
    {
        _enemies = _enemyChecker.Check<IDamageable>();

        if (_enemies != null)
            UpdateCrossHair();
    }

    public void UpdateCrossHair()
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

    private void DisableCrossHair()
    {
        _crosshair.gameObject.SetActive(false);
    }

    public IDamageable FindClosestLivingEnemy()
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

    private void UpdateCrossHairPosition(IDamageable target)
    {
        if (target == null)
            return;

        _crosshair.transform.parent = target.TargetTransform;
        _crosshair.transform.localPosition = Vector3.zero;
    }
}
