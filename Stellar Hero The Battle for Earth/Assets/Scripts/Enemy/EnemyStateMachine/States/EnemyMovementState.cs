using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyUnit))]
public class EnemyMovementState : State
{
    protected Animator _animator;
    private float _speed;
    private EnemyUnit _enemyUnit;
    private NavMeshAgent _agent;
   
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _animator = GetComponent<Animator>();
        _enemyUnit = GetComponent<EnemyUnit>();
        _speed = _enemyUnit.Config.Speed;
    }

    private void Update()
    {
        if (StateManager.Instance.CurrentGameState == GameStates.Paused)
        {
            _agent.destination = _agent.transform.position;
            return;
        }

        if (Target != null)
            _agent.SetDestination(Target.TargetTransform.position);

        if(Health.CurrentHealth == 0)
            _agent.destination = _agent.transform.position;
    }
}
