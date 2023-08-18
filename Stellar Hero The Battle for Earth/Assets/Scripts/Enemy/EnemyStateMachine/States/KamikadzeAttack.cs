using UnityEngine;

[RequireComponent(typeof(Animator))]
public class KamikadzeAttack : AttackState
{
    [SerializeField] private ParticleSystem _explodeEffect;
    private float _explosionRadius = 2f;

    public override void Attack()
    {
        if (Target != null)
        {
            if (Vector2.Distance(Target.TargetTransform.position, transform.position) < 1)
            {
                _animator.Play(Constants.ExplosionEnemyAnimation);
                _explodeEffect.Play();
                Explode();
                gameObject.SetActive(false);
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
    }
}
