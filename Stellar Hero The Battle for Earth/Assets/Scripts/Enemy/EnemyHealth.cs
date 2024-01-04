using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyUnit)), RequireComponent(typeof(DeadEffectSpawner)), RequireComponent(typeof(DamagePopuper)), RequireComponent(typeof(EnemyBuffDropper))]
public class EnemyHealth : UnitHealth, IDamageable
{
    private MaterialBlicker _blicker;
    private DamagePopuper _damagePopuper;
    private DeadEffectSpawner _deadEffectSpawner;
    private EnemyBuffDropper _enemyBuffDropper;

    public event Action<EnemyHealth> Dying;

    public float CurrentHealth
    {
        get
        {
            return �urrenHealth;
        }
        private set
        {
            �urrenHealth = Mathf.Clamp(value, 0, _maxHealth);

            if (�urrenHealth <= 0)
                Die();
        }
    }

    public Transform TargetTransform => transform;

    public bool IsAlive => �urrenHealth > 0;

    private void Start()
    {
        _enemyBuffDropper = GetComponent<EnemyBuffDropper>();
        _deadEffectSpawner = GetComponent<DeadEffectSpawner>();
        _damagePopuper = GetComponent<DamagePopuper>();
        _blicker = GetComponentInChildren<MaterialBlicker>();
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0)
            throw new ArgumentException("Value cannot be negative", nameof(damage));

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
        var timeForDisable = 0.5f;
        var waitForSeconds = new WaitForSeconds(timeForDisable);
        yield return waitForSeconds;
        gameObject.SetActive(false);
    }
}
