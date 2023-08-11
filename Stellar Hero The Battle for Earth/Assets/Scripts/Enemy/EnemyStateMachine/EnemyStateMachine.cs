using UnityEngine;

[RequireComponent(typeof(EnemyUnit))]
public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private State _firstState;

    public Transform Target { get; private set; }

    public State CurrentState { get; private set; }

    private void Start()
    {
        ResetState();
    }

    private void Update()
    {
        if (CurrentState == null)
            return;

        var nextState = CurrentState.GetNextState();

        if(nextState != null)
            Transit(nextState);
    }

    public void SetTarget(Transform target)
    {
        if (target != null)
            Target = target;

        Debug.Log(Target.position);
    }

    public void ResetState()
    {
        CurrentState = _firstState;

        if (CurrentState != null)
            CurrentState.Enter(Target);
    }

    private void Transit(State nextState)
    {
        if (CurrentState != null)
            CurrentState.Exit();

        CurrentState = nextState;

        if (CurrentState != null)
            CurrentState.Enter(Target);
    }
}
