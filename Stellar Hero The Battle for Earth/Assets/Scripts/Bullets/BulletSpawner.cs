using Assets.Scripts.Components.Checkers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BulletParams))]
public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private Bullet _tamplate;
    [SerializeField] private CheckCircleOverlap _enemyChecker;
    [SerializeField] private CrossHair _crosshair;

    private List<GameObject> _enemies;
    private Vector2 _directionToTarget;
    private GameObject _currentTarget;

    private BulletParams _params;
    private Vector3 _shotTarget;

    private bool _isShooting => _currentTarget != null;

    private void Awake()
    {
        _params = GetComponent<BulletParams>();
    }

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private void Update()
    {
        _enemies = _enemyChecker.Check();
        UpdateCrossHair();
    }

    private IEnumerator Spawn()
    {
        while (StateManager.Instance.CurrentGameState == GameStates.Gameplay)
        {
            if (_isShooting)
            {
                //RotateToTarget(_currentTarget.transform.position);
                for (int i = 0; i < _params.Count; i++)
                {
                    var bullet = Instantiate(_tamplate);

                    CalculateShotTarget(i - 1);

                    bullet.Shot(gameObject.transform.position, _shotTarget, _params.BulletSpeed, _params.Damage);
                }

                yield return new WaitForSeconds(_params.AttackCooldown);
            }

            yield return null;
        }

    }

    private void CalculateShotTarget(int number)
    {
        int sideModifier = number % 2 == 0 ? -1 : 1;
        var dispertion = _params.StepDispertion * number * sideModifier;
        Quaternion deviation = Quaternion.Euler(dispertion, dispertion, 0f);
        _shotTarget = deviation * _currentTarget.transform.position;
    }

    private void RotateToTarget(Vector3 target)
    {
        _directionToTarget = target - transform.position;
        float angle = Mathf.Atan2(_directionToTarget.y, _directionToTarget.x) * Mathf.Rad2Deg + 90f;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void UpdateCrossHair()
    {
        if (_currentTarget == null)
        {
            DisableCrossHair();
            _currentTarget = FindClosestLivingEnemy();
        }
        else
        {
            if (_currentTarget.GetComponent<EnemyHealth>().CurrentHealth <= 0)
                _currentTarget = FindClosestLivingEnemy();

            if (_currentTarget == null) return;

            if (!_crosshair.gameObject.activeSelf)
                _crosshair.gameObject.SetActive(true);

            UpdateCrossHairPosition(_currentTarget);
        }
    }

    private void DisableCrossHair()
    {
        _crosshair.gameObject.SetActive(false);
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
        _crosshair.transform.parent = target.transform;
        _crosshair.transform.localPosition = Vector3.zero;
        //Debug.Log($"Crosshair position: {_crosshair.transform.position}, Target position: {vector}");
    }
}
