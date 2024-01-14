using UnityEngine;
using UnityEngine.AI;
using Utils;

namespace Enemy
{
    [RequireComponent(typeof(EnemyUnit)), RequireComponent(typeof(NavMeshAgent)), RequireComponent(typeof(Animator))]
    public class EnemyMovementState : State
    {
        protected Animator Animator;
        private EnemyUnit _enemyUnit;
        private NavMeshAgent _agent;
        private float _speed;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            Animator = GetComponent<Animator>();
            _enemyUnit = GetComponent<EnemyUnit>();
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
            _speed = _enemyUnit.Config.Speed;
            _agent.speed = _speed;
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

            if (Health.CurrentHealth == 0)
                _agent.destination = _agent.transform.position;
        }
    }
}
