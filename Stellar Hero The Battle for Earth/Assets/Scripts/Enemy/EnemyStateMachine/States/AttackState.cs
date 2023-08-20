using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AttackState : State
{
    [SerializeField] protected int _damage;
    [SerializeField] protected float _delay;

    protected EnemyStateMachine _enemyStateMachine;
    protected Animator _animator;
    protected float _lastAttackTime;

    protected void Start()
    {
        _enemyStateMachine = GetComponent<EnemyStateMachine>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(_lastAttackTime <= 0)
        {
            Attack();
            _lastAttackTime = _delay;
        }

        _lastAttackTime -= Time.deltaTime;
    }

    public virtual void Attack()
    {
        if(Target.IsAlive) 
        {
            if(Vector2.Distance(Target.TargetTransform.position, transform.position) < 1)
            {
                _animator.Play(Constants.AttackState);
                Target.TakeDamage(_damage);
            }
            else
            {
                enabled = false;
                _enemyStateMachine.ResetState();
            }
        }
    }
}
