using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(DeadEffectSpawner))]
public class KamikadzeAttack : AttackState
{
    [SerializeField] private ParticleSystem _explosionEffect;
    private DeadEffectSpawner _effectSpawner;
    private readonly float _explosionRadius = 1f;
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
            if (Vector2.Distance(Target.TargetTransform.position, transform.position) < 1)
            {
                Explode();
            }
            else
            {
                _enemyStateMachine.ResetState();
                enabled = false;
            }
        }
    }

    private void Explode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _explosionRadius);

        foreach (Collider2D collider in colliders)
        {
            var unit = collider.GetComponent<IDamageable>();
            unit?.TakeDamage(_damage);
        }

        _effectSpawner.SpawnEffect(_explosionEffect);
        StartCoroutine(DisableGameObject());
    }

    private IEnumerator DisableGameObject()
    {
        float disableDealy = 1.8f;
        var waitForSeconds = new WaitForSeconds(disableDealy);
        yield return waitForSeconds;
        _enemyHealth.TakeDamage(500);
    }
}
