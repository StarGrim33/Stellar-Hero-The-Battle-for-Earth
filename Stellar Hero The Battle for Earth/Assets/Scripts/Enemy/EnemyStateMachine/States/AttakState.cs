using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AttakState : State
{
    [SerializeField] private int _damage;
    [SerializeField] private float _delay;
    
    private EnemyStateMachine _enemyStateMachine;
    private Animator _animator;
    private float _lastAttackTime;
    private IDamageable _target;

    private void Start()
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

    private void Attack()
    {
        if(_target != null) 
        {
            if(Vector2.Distance(Target.transform.position, transform.position) < 0.7)
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
