using UnityEngine;

public class RangeAttackState : AttackState
{
    private readonly float _speed = 3.5f;
    [SerializeField] private EnemyBullet _bullet;

    public override void Attack()
    {
        float minRangeAttack = 10f;

        if (Target.IsAlive)
        {
            if (Vector2.Distance(Target.TargetTransform.position, transform.position) < minRangeAttack)
            {
                Animator.Play(Constants.AttackState);
                SpawnBullet();
            }
            else
            {
                enabled = false;
                EnemyStateMachine.ResetState();
            }
        }
    }

    private void SpawnBullet()
    {
        Vector2 directionToTarget = (Target.TargetTransform.position - transform.position).normalized;

        var bullet = Instantiate(_bullet);
        bullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, -directionToTarget);
        bullet.Shot(transform.position, Target.TargetTransform.position, _speed, Damage);
    }
}
