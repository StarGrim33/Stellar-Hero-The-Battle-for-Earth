using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Pistol : Weapon
{
    [SerializeField] private PoolObjectSpawnComponent _spawnComponent;
    [SerializeField] private BulletParams _params;
    [SerializeField] private Transform _transform;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _shotSound;
    [SerializeField] private AudioClip _reloadSound;
    [SerializeField] private CinemachineImpulseSource _cameraShaker;

    public event UnityAction<bool> Reloading;

    private PlayerCharacteristics _characteristics;

    private void Start()
    {
        _characteristics = PlayerCharacteristics.I;
    }

    protected override void RotateWeaponToTarget(Vector3 target)
    {
        Vector2 directionToTarget = (target - transform.position).normalized;
        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
        _weaponSprite.transform.eulerAngles = new Vector3(0, 0, angle);

        if (directionToTarget.x < 0)
        {
            _weaponSprite.flipX = false;
            _weaponSprite.flipY = true;
        }
        else
        {
            _weaponSprite.flipX = false;
            _weaponSprite.flipY = false;
        }
    }

    protected override void DisableCrossHair()
    {
        _crosshair.SetActive(false);
    }

    protected override void StartReloading()
    {
        if (_isReloading == false && _currentAmmo < _maxAmmo)
        {
            _isReloading = true;
            _audioSource.PlayOneShot(_reloadSound);
            Reloading?.Invoke(_isReloading);
            StartCoroutine(ReloadCoroutine());
        }
    }

    protected override void UpdateCrossHairPosition(IDamageable target)
    {
        if (target == null)
            return;

        _crosshair.transform.parent = target.TargetTransform;
        _crosshair.transform.localPosition = Vector3.zero;
    }

    protected override void SpawnBullet()
    {
        Vector2 directionToTarget = (_currentTarget.TargetTransform.position - transform.position).normalized;

        GameObject gameObject = _spawnComponent.Spawn();

        if (gameObject.TryGetComponent(out Bullet bullet))
        {
            bullet.gameObject.SetActive(true);
            bullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, -directionToTarget);
            bullet.Shot(_transform.position, _currentTarget.TargetTransform.position, _params.BulletSpeed, (int)_characteristics.GetValue(Characteristics.Damage));

            _cameraShaker.GenerateImpulse();
            _audioSource.PlayOneShot(_shotSound);

        }
    }

    private IEnumerator ReloadCoroutine()
    {
        var waitForSeconds = new WaitForSeconds(_reloadTime);
        yield return waitForSeconds;
        _currentAmmo = _maxAmmo;
        _isReloading = false;
        Reloading?.Invoke(_isReloading);
    }
}
