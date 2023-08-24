using Assets.Scripts.Components.Checkers;
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CheckCircleOverlap))]
public class DroneStateMachine : MonoBehaviour
{
    [SerializeField] private Transform _playerUnit;
    private Dictionary<Type, IStateSwitcher> _states;
    private IStateSwitcher _currentState;
    private CheckCircleOverlap _enemyChecker;
    private ParticleSystemPlayer _shotEffect;

    private void Start()
    {
        _shotEffect = GetComponent<ParticleSystemPlayer>();
        _enemyChecker = GetComponent<CheckCircleOverlap>();
        Init();
        SetBehaviourByDefault();
    }

    private void Update()
    {
        _currentState?.Update();
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
            [typeof(DroneMovementState)] = new DroneMovementState(transform, _playerUnit, this, _enemyChecker),
            [typeof(DroneAttackState)] = new DroneAttackState(this, _enemyChecker, _shotEffect, transform),
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
