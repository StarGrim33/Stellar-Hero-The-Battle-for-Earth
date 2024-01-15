using System;
using System.Collections;
using Bullets;
using Cinemachine;
using Player;
using Plugins.Audio.Core;
using UnityEngine;
using Utils;

namespace Weapon
{
    public class Pistol : BaseWeapon
    {
        [SerializeField] private PoolObjectSpawnComponent _spawnComponent;
        [SerializeField] private BulletSpeedModifier _params;
        [SerializeField] private Transform _transform;
        [SerializeField] private SourceAudio _audioSource;
        [SerializeField] private CinemachineImpulseSource _cameraShaker;
        private PlayerCharacteristics _characteristics;
        private WaitForSeconds _delay;

        public event Action<bool> Reloading;

        private void Start()
        {
            Init();
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
            Crosshair.gameObject.SetActive(false);
        }

        protected override void StartReloading()
        {
            if (!_isReloading && _currentAmmo < _maxAmmo)
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

            if (gameObject.TryGetComponent(out PlayerBullet bullet))
            {
                bullet.gameObject.SetActive(true);
                bullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, -directionToTarget);
                bullet.Shot(_transform.position, _currentTarget.TargetTransform.position, _params.GetBulletSpeed(), (int)_characteristics.GetValue(Characteristics.Damage));
                _cameraShaker.GenerateImpulse();
                _audioSource.PlayOneShot(Constants.ShotSound);
            }
        }

        private IEnumerator ReloadCoroutine()
        {
            yield return _delay;
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

        private void Init()
        {
            _delay = new WaitForSeconds(_reloadTime);
            _characteristics = PlayerCharacteristics.I;
            _characteristics.CharacteristicChanged += SetWeaponParams;
            SetWeaponParams();
            _currentAmmo = _maxAmmo;
        }
    }
}