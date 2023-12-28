using Cinemachine;
using Plugins.Audio.Core;
using System;
using System.Collections;
using UnityEngine;

public class Pistol : Weapon
{
    [SerializeField] private PoolObjectSpawnComponent _spawnComponent;
    [SerializeField] private BulletSpeedModifier _params;
    [SerializeField] private Transform _transform;
    [SerializeField] private SourceAudio _audioSource;
    [SerializeField] private CinemachineImpulseSource _cameraShaker;
    private PlayerCharacteristics _characteristics;

    public event Action<bool> Reloading;

    private void Start()
    {
        _characteristics = PlayerCharacteristics.I;
        _characteristics.CharacteristicChanged += SetWeaponParams;
        SetWeaponParams();
        _currentAmmo = _maxAmmo;
    }

    protected override void RotateWeaponToTarget(Vector3 target)
    {
        Vector2 directionToTarget = (target - transform.position).normalized;
        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
        WeaponSprite.transform.eulerAngles = new Vector3(0, 0, angle);

        if (directionToTarget.x < 0)
        {
            WeaponSprite.flipX = false;
            WeaponSprite.flipY = true;
        }
        else
        {
            WeaponSprite.flipX = false;
            WeaponSprite.flipY = false;
        }
    }

    protected override void DisableCrossHair()
    {
        Crosshair.SetActive(false);
    }

    protected override void StartReloading()
    {
        if (_isReloading == false && _currentAmmo < _maxAmmo)
        {
            _isReloading = true;
            _audioSource.PlayOneShot(Constants.ReloadSound);
            Reloading?.Invoke(_isReloading);
            StartCoroutine(ReloadCoroutine());
        }
    }

    protected override void UpdateCrossHairPosition(IDamageable target)
    {
        if (target == null)
            return;

        Crosshair.transform.parent = target.TargetTransform;
        Crosshair.transform.localPosition = Vector3.zero;
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
            _audioSource.PlayOneShot(Constants.ShotSound);
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

    private void SetWeaponParams()
    {
        _reloadTime = _characteristics.GetValue(Characteristics.ReloadTimeAmmo);
        _maxAmmo = (int)_characteristics.GetValue(Characteristics.MaxAmmo);
        ShotCooldown.ChangeCooldownValue(_characteristics.GetValue(Characteristics.ShotCooldown));
    }
}
