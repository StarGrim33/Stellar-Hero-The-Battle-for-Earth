using Core;
using System;
using System.Collections;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(EnemyUnit)), RequireComponent(typeof(DeadEffectSpawner)),
    RequireComponent(typeof(DamagePopuper)), RequireComponent(typeof(EnemyBuffDropper))]
    public class EnemyHealth : UnitHealth, IDamageable
    {
        private readonly float _timeForEnemyDisable = 0.5f;
        private MaterialBlicker _blicker;
        private DamagePopuper _damagePopuper;
        private DeadEffectSpawner _deadEffectSpawner;
        private EnemyBuffDropper _enemyBuffDropper;
        private WaitForSeconds _delay;

        public event Action<EnemyHealth> Dying;

        public float CurrentHealth
        {
            get
            {
                return ÑurrenHealth;
            }
            private set
            {
                ÑurrenHealth = Mathf.Clamp(value, 0, MaxHealth);

                if (ÑurrenHealth <= 0)
                    Die();
            }
        }

        public Transform TargetTransform => transform;

        public bool IsAlive => ÑurrenHealth > 0;

        private void Start()
        {
            _delay = new WaitForSeconds(_timeForEnemyDisable);
            Init();
        }

        public void TakeDamage(int damage)
        {
            if (damage <= 0)
                throw new ArgumentException(nameof(damage));

            _deadEffectSpawner?.SpawnRandomEffect();
            CurrentHealth -= damage;
            _blicker?.Flash();
            _damagePopuper?.ShowDamagePopup(damage);
        }

        protected override void Die()
        {
            _enemyBuffDropper?.SpawnRandomBuff();
            Dying?.Invoke(this);
            StartCoroutine(SetDisabled());
        }

        private IEnumerator SetDisabled()
        {
            yield return _delay;
            gameObject.SetActive(false);
        }

        private void Init()
        {
            _enemyBuffDropper = GetComponent<EnemyBuffDropper>();
            _deadEffectSpawner = GetComponent<DeadEffectSpawner>();
            _damagePopuper = GetComponent<DamagePopuper>();
            _blicker = GetComponentInChildren<MaterialBlicker>();
        }
    }
}
