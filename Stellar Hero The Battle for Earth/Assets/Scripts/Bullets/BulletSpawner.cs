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
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private CameraShaker _cameraShaker;

    private List<IDamageable> _enemies;
    private Vector2 _directionToTarget;
    private IDamageable _currentTarget;

    private BulletParams _params;
    private Vector3 _shotTarget;
    private Coroutine _coroutine;

    private bool _isShooting => _currentTarget != null;

    private void Awake()
    {
        _params = GetComponent<BulletParams>();
    }

    private void Start()
    {
        _coroutine = StartCoroutine(Spawn());
    }

    private void Update()
    {
        _enemies = _enemyChecker.Check<IDamageable>();
        UpdateCrossHair();
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            if (StateManager.Instance.CurrentGameState == GameStates.Gameplay && _isShooting)
            {
                for (int i = 0; i < _params.Count; i++)
                {
                    var bullet = Instantiate(_tamplate);
                    CalculateShotTarget(i - 1);
                    bullet.Shot(gameObject.transform.position, _shotTarget, _params.BulletSpeed, _params.Damage);
                    _audioSource.PlayOneShot(_audioSource.clip);
                    _cameraShaker.Shake();
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
        _shotTarget = deviation * _currentTarget.TargetTransform.position;
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
            if (_currentTarget.IsAlive == false)
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

    private IDamageable FindClosestLivingEnemy()
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
        _crosshair.transform.parent = target.TargetTransform;
        _crosshair.transform.localPosition = Vector3.zero;
    }
}