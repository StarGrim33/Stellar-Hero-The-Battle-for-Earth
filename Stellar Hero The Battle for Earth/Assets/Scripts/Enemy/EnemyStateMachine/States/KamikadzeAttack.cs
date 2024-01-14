using System.Collections;
using Core;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Animator), typeof(DeadEffectSpawner))]
    public class KamikadzeAttack : AttackState, IBomber
    {
        private readonly int _explosionDamage = 25;
        private readonly int _maxExplosionDamage = 500;
        private readonly float _explosionRadius = 1f;
        private readonly float _explosionTimeDelay = 0.5f;
        private readonly float _attackDistance = 0.8f;
        private readonly float _disableTimeDelay = 1.8f;

        [SerializeField] private ParticleSystem _explosionEffect;
        private DeadEffectSpawner _effectSpawner;
        private EnemyHealth _enemyHealth;
        private WaitForSeconds _explosionDelay;
        private WaitForSeconds _disableDelay;

        private void Awake()
        {
            _enemyHealth = GetComponent<EnemyHealth>();
            _effectSpawner = GetComponent<DeadEffectSpawner>();
            _explosionDelay = new WaitForSeconds(_explosionTimeDelay);
            _disableDelay = new WaitForSeconds(_disableTimeDelay);
        }

        public override void Attack()
        {
            if (Target != null)
            {
                if (Vector2.Distance(Target.TargetTransform.position, transform.position) < _attackDistance)
                {
                    StartCoroutine(ExplodeWithDelay());
                }
                else
                {
                    EnemyStateMachine.ResetState();
                    enabled = false;
                }
            }
        }

        public void Explode()
        {
            foreach (Collider2D collider in Physics2D.OverlapCircleAll(transform.position, _explosionRadius))
            {
                var unit = collider.GetComponent<IDamageable>();
                unit?.TakeDamage(_explosionDamage);
            }

            _effectSpawner.SpawnEffect(_explosionEffect);
            StartCoroutine(DisableGameObject());
        }

        private IEnumerator ExplodeWithDelay()
        {
            yield return _explosionDelay;
            Explode();
        }

        private IEnumerator DisableGameObject()
        {
            yield return _disableDelay;
            _enemyHealth.TakeDamage(_maxExplosionDamage);
        }
    }
}
