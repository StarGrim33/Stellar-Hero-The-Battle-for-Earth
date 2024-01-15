using Utils;
using UnityEngine;

namespace Enemy
{
    public class EnemyStateMachine : MonoBehaviour
    {
        [SerializeField] private State _firstState;

        public IDamageable Target { get; private set; }

        public State CurrentState { get; private set; }

        private void Start()
        {
            ResetState();
        }

        private void Update()
        {
            if (CurrentState == null || StateManager.Instance.CurrentGameState == GameStates.Paused)
                return;

            var nextState = CurrentState.GetNextState();

            if (nextState != null)
                Transit(nextState);
        }

        public void SetTarget(IDamageable target)
        {
            if (target != null)
                Target = target;
        }

        public void ResetState()
        {
            CurrentState = _firstState;
            CurrentState?.Enter(Target);
        }

        private void Transit(State nextState)
        {
            CurrentState?.Exit();
            CurrentState = nextState;
            CurrentState?.Enter(Target);
        }
    }
}
