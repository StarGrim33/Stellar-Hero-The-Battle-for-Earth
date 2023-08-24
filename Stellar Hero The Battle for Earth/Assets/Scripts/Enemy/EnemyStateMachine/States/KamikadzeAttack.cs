using UnityEngine;

[RequireComponent(typeof(Animator))]
public class KamikadzeAttack : AttackState
{
    private float _explosionRadius = 1f;

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
    }
}
