using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(DeadEffectSpawner))]
public class KamikadzeAttack : AttackState
{
    private readonly float _explosionRadius = 1f;
    private readonly int _explosionDamage = 25;
    private readonly float _explosionDelay = 0.5f;
    private readonly float _attackDistance = 0.8f;

    [SerializeField] private ParticleSystem _explosionEffect;

    private DeadEffectSpawner _effectSpawner;
    private EnemyHealth _enemyHealth;

    private void Awake()
    {
        _enemyHealth = GetComponent<EnemyHealth>();
        _effectSpawner = GetComponent<DeadEffectSpawner>();
    }

    public override void Attack()
    {
        if (Target != null)
        {
            if (Vector2.Distance(Target.TargetTransform.position, transform.position) < _attackDistance)
            {
                StartCoroutine(ExplodeWithDelay(_explosionDelay));
            }
            else
            {
                EnemyStateMachine.ResetState();
                enabled = false;
            }
        }
    }

    private IEnumerator ExplodeWithDelay(float delay)
    {
        var waitForSeconds = new WaitForSeconds(delay);
        yield return waitForSeconds; 
        Explode();
    }

    private void Explode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _explosionRadius);

        foreach (Collider2D collider in colliders)
        {
            var unit = collider.GetComponent<IDamageable>();
            unit?.TakeDamage(_explosionDamage);
        }

        _effectSpawner.SpawnEffect(_explosionEffect);
        StartCoroutine(DisableGameObject());
    }

    private IEnumerator DisableGameObject()
    {
        int maxDamage = 500;
        float disableDelay = 1.8f;
        var waitForSeconds = new WaitForSeconds(disableDelay);
        yield return waitForSeconds;
        _enemyHealth.TakeDamage(maxDamage);
    }
}
