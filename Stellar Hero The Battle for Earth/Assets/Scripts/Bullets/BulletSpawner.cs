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
    private float fullAngle = 360f;

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
                SimpleShot();

                CircleShot();

                yield return new WaitForSeconds(_params.AttackCooldown);
            }

            yield return null;
        }

    }

    private void CircleShot()
    {
        int circleCount = _params.CircleCount;
        float angleStep = fullAngle / circleCount;
        float firstShotAngle = Random.Range(0, fullAngle);

        for (int i = 0; i < circleCount; i++)
        {
            var bullet = Instantiate(_tamplate);
            CalculateCircleShotTarget(i - 1, firstShotAngle, angleStep);

            bullet.Shot(gameObject.transform.position, _shotTarget, _params.BulletSpeed, _params.Damage);
        }
    }

    private void SimpleShot()
    {
        for (int i = 0; i < _params.Count; i++)
        {
            var bullet = Instantiate(_tamplate);

            CalculateSimpleShotTarget(i-1);

            bullet.Shot(gameObject.transform.position, _shotTarget, _params.BulletSpeed, _params.Damage);
        }
    }

    private void CalculateCircleShotTarget(int number, float firstShotAngle, float angleStep)
    {
        float angle = firstShotAngle + number * angleStep;

        float x = Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = Mathf.Sin(angle * Mathf.Deg2Rad);

        _shotTarget = transform.position + new Vector3(x, y, 0f);
    }

    private void CalculateSimpleShotTarget(int number)
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
