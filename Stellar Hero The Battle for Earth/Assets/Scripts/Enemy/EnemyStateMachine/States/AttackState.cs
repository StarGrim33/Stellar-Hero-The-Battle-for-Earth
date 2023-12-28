using UnityEngine;

[RequireComponent(typeof(Animator)), RequireComponent(typeof(EnemyStateMachine))]
public class AttackState : State
{
    [SerializeField] protected int Damage;
    [SerializeField] protected float Delay;

    protected EnemyStateMachine EnemyStateMachine;
    protected Animator Animator;
    protected float LastAttackTime;

    protected void Start()
    {
        EnemyStateMachine = GetComponent<EnemyStateMachine>();
        Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(LastAttackTime <= 0)
        {
            Attack();
            LastAttackTime = Delay;
        }

        LastAttackTime -= Time.deltaTime;
    }

    public virtual void Attack()
    {
        int minDistanceForAttack = 1;

        if(Target.IsAlive) 
        {
            if(Vector2.Distance(Target.TargetTransform.position, transform.position) < minDistanceForAttack)
            {
                Animator.Play(Constants.AttackState);
                Target.TakeDamage(Damage);
            }
            else
            {
                enabled = false;
                EnemyStateMachine.ResetState();
            }
        }
    }
}
