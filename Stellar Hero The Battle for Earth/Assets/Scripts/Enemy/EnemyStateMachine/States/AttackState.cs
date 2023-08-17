using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AttackState : State
{
    [SerializeField] protected int _damage;
    [SerializeField] protected float _delay;

    protected EnemyStateMachine _enemyStateMachine;
    protected Animator _animator;
    protected float _lastAttackTime;
    protected IDamageable _target;

    protected void Start()
    {
        _enemyStateMachine = GetComponent<EnemyStateMachine>();
        _target = Target.GetComponent<IDamageable>();
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
        if(_target != null) 
        {
            if(Vector2.Distance(Target.transform.position, transform.position) < 1)
            {
                _animator.Play(Constants.AttackState);
                _target.TakeDamage(_damage);
            }
            else
            {
                _enemyStateMachine.ResetState();
                enabled = false;
            }
        }
    }
}
