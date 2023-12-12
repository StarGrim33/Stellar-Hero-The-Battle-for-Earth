using UnityEngine;

public class RangeAttackState : AttackState
{
    [SerializeField] private EnemyBullet _bullet;
    private float _speed = 3.5f;

    public override void Attack()
    {
        if (Target.IsAlive)
        {
            if (Vector2.Distance(Target.TargetTransform.position, transform.position) < 10f)
            {
                _animator.Play(Constants.AttackState);
                SpawnBullet();
            }
            else
            {
                enabled = false;
                _enemyStateMachine.ResetState();
            }
        }
    }

    private void SpawnBullet()
    {
        Vector2 directionToTarget = (Target.TargetTransform.position - transform.position).normalized;

        var bullet = Instantiate(_bullet);
        bullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, -directionToTarget);
        bullet.Shot(transform.position, Target.TargetTransform.position, _speed, _damage);
    }
}
