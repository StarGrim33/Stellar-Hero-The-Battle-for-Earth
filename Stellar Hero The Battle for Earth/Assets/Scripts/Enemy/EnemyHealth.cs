using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyUnit))]
public class EnemyHealth : UnitHealth, IDamageable
{
    private MaterialBlicker _blicker;
    private DamagePopuper _damagePopuper;
    private DeadEffectSpawner _deadEffectSpawner;
    private EnemyBuffDropper _enemyBuffDropper;
    public event UnityAction<EnemyHealth> Dying;

    public float MaxHealth { get; private set; }

    public float CurrentHealth
    {
        get
        {
            return ÑurrenHealth;
        }
        private set
        {
            ÑurrenHealth = Mathf.Clamp(value, 0, _maxHealth);

            if (ÑurrenHealth <= 0)
                Die();
        }
    }

    private void Start()
    {
        _enemyBuffDropper = GetComponent<EnemyBuffDropper>();
        _deadEffectSpawner = GetComponent<DeadEffectSpawner>();
        _damagePopuper = GetComponent<DamagePopuper>();
        _blicker = GetComponentInChildren<MaterialBlicker>();
    }

    public Transform TargetTransform => transform;

    public bool IsAlive => ÑurrenHealth > 0;

    public void TakeDamage(int damage)
    {
        if (damage <= 0)
            throw new ArgumentException("Value cannot be negative", nameof(damage));

        //if (_deadEffectSpawner != null)
        //    _deadEffectSpawner.SpawnEffect();

        if (_deadEffectSpawner != null)
            _deadEffectSpawner.SpawnRandomEffect();

        CurrentHealth -= damage;

        if (_blicker != null)
            _blicker.Flash();

        if (_damagePopuper != null)
            _damagePopuper.ShowDamagePopup(damage);
    }

    protected override void Die()
    {


        if (_enemyBuffDropper != null)
            _enemyBuffDropper.SpawnRandomBuff();

        Dying?.Invoke(this);

        StartCoroutine(SetDisabled());
    }

    private IEnumerator SetDisabled()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
