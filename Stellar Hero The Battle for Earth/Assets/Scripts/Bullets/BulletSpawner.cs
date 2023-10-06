using Assets.Scripts.Components.Checkers;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private Bullet _template;
    [SerializeField] private CheckCircleOverlap _enemyChecker;
    [SerializeField] private CrossHair _crosshair;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private CameraShaker _cameraShaker;

    private List<IDamageable> _enemies;
    private IDamageable _currentTarget;

    private BulletParams _params;
    private Vector3 _shotTarget;
    private Coroutine _coroutine;

    private bool _isShooting => _currentTarget != null;

    private void Awake()
    {
        _params = GetComponent<BulletParams>();
    }

    private void Update()
    {
        _enemies = _enemyChecker.Check<IDamageable>();

        if (_enemies != null)
            UpdateCrossHair();
    }

    //private IEnumerator Spawn()
    //{
    //    while (true)
    //    {
    //        if (StateManager.Instance.CurrentGameState == GameStates.Gameplay && _isShooting)
    //        {
    //            for (int i = 0; i < _params.Count; i++)
    //            {
    //                var bullet = Instantiate(_template);
    //                //CalculateShotTarget(i - 1);
    //                bullet.Shot(transform.position, _shotTarget, _params.BulletSpeed, _params.Damage);
    //                _audioSource.PlayOneShot(_audioSource.clip);
    //                _cameraShaker.Shake();
    //            }

    //            yield return new WaitForSeconds(_params.AttackCooldown);
    //        }

    //        yield return null;
    //    }
    //}

    //private void CalculateShotTarget(int number)
    //{
    //    int sideModifier = number % 2 == 0 ? -1 : 1;
    //    var dispertion = _params.StepDispertion * number * sideModifier;
    //    Quaternion deviation = Quaternion.Euler(dispertion, dispertion, 0f);
    //    _shotTarget = deviation * _currentTarget.TargetTransform.position;
    //}

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
