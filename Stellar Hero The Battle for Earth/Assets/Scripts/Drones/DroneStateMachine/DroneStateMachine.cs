using System;
using System.Collections.Generic;
using UnityEngine;

public class DroneStateMachine : MonoBehaviour
{
    [SerializeField] private Transform _playerUnit;
    private Dictionary<Type, IStateSwitcher> _states;
    private IStateSwitcher _currentState;
    private Transform _droneTransform;

    private void Start()
    {
        _droneTransform = transform;
        Init();
        SetBehaviourByDefault();
    }

    private void Update()
    {
        _currentState?.Update();
    }

    public void SetIdleState()
    {
        var state = GetBehaviour<DroneIdleState>();
        SetBehaviour(state);
    }

    public void SetMovementState()
    {
        var state = GetBehaviour<DroneMovementState>();
        SetBehaviour(state);
    }

    public void SetWorkState()
    {
        var state = GetBehaviour<DroneAttackState>();
        SetBehaviour(state);
    }

    private void Init()
    {
        _states = new Dictionary<Type, IStateSwitcher>()
        {
            [typeof(DroneIdleState)] = new DroneIdleState(),
            [typeof(DroneMovementState)] = new DroneMovementState(transform, _playerUnit),
            [typeof(DroneAttackState)] = new DroneAttackState(),
        };
    }

    private void SetBehaviour(IStateSwitcher state)
    {
        _currentState?.Exit();
        _currentState = state;
        _currentState.Enter();
    }

    private void SetBehaviourByDefault()
    {
        var state = GetBehaviour<DroneMovementState>();
        SetBehaviour(state);
    }

    private IStateSwitcher GetBehaviour<T>() where T: IStateSwitcher
    {
        return _states[typeof(T)];
    }
}
