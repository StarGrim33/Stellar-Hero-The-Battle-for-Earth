using Assets.Scripts.Components.Checkers;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Drone
{
    [RequireComponent(typeof(CheckCircleOverlap), typeof(DroneSpriteRotator),
    typeof(DroneParticleSystem)), RequireComponent(typeof(CheckCircleOverlap))]
    public class DroneStateMachine : MonoBehaviour
    {
        [SerializeField] private Transform _playerUnit;
        private Dictionary<Type, IStateSwitcher> _states;
        private IStateSwitcher _currentState;
        private CheckCircleOverlap _enemyChecker;
        private DroneParticleSystem _shotEffect;
        private DroneSpriteRotator _spriteRotator;
        private DroneParameters _parameters;
        private TrailInstantiator _instantiator;
        private PlayerCharacteristics _characteristics;

        private void Start()
        {
            Init();
            SetBehaviourByDefault();
            SpriteRotatorInit();
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
            _instantiator = GetComponent<TrailInstantiator>();
            _spriteRotator = GetComponent<DroneSpriteRotator>();
            _shotEffect = GetComponent<DroneParticleSystem>();
            _enemyChecker = GetComponent<CheckCircleOverlap>();
            _parameters = new DroneParameters();
            _characteristics = PlayerCharacteristics.I;

            _states = new Dictionary<Type, IStateSwitcher>()
            {
                [typeof(DroneMovementState)] = new DroneMovementState(transform, _playerUnit, this, _enemyChecker, _shotEffect),
                [typeof(DroneAttackState)] = new DroneAttackState(this, _enemyChecker, _shotEffect, transform,
                _playerUnit, _parameters, _instantiator, _characteristics),
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

        private IStateSwitcher GetBehaviour<T>() where T : IStateSwitcher
        {
            return _states[typeof(T)];
        }

        private void SpriteRotatorInit()
        {
            if (_states.TryGetValue(typeof(DroneAttackState), out var attackState))
            {
                if (attackState is DroneAttackState droneAttackState)
                {
                    _spriteRotator.Init(droneAttackState);
                }
            }
        }
    }
}