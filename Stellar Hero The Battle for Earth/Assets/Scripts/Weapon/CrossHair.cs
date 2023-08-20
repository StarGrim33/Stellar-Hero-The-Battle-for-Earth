using Assets.Scripts.Components.Checkers;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    [SerializeField] protected CheckCircleOverlap _enemyChecker;

    private List<GameObject> _enemies;
    private GameObject _target;

    public GameObject Target => _target;

    private void Update()
    {
        _enemies = _enemyChecker.Check();
        UpdateCrossHair();
    }

    private void UpdateCrossHair()
    {
        if (_target == null)
        {
            DisableCrossHair();
            _target = FindClosestLivingEnemy();
        }
        else
        {
            if (_target.GetComponent<EnemyHealth>().CurrentHealth <= 0)
                _target = FindClosestLivingEnemy();

            if (_target == null) return;

            //if (!gameObject.activeSelf)
            //    gameObject.SetActive(true);

            UpdateCrossHairPosition(_target);
        }
    }

    private void DisableCrossHair()
    {
        _target = null;
        //gameObject.SetActive(false);
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

    private void UpdateCrossHairPosition(GameObject target)
    {
        transform.position = target.transform.position;
        //Debug.Log($"Crosshair position: {_crosshair.transform.position}, Target position: {vector}");
    }
}
