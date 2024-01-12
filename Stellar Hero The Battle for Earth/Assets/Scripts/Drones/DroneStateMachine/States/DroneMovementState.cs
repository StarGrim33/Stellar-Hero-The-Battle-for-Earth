using Assets.Scripts.Components.Checkers;
using UnityEngine;

namespace Drone
{
    public class DroneMovementState : IDroneMovementState, IStateSwitcher
    {
        private Transform _droneTransform;
        private Transform _heroTransform;
        private DroneStateMachine _stateMachine;
        private CheckCircleOverlap _enemyChecker;
        private DroneParticleSystem _enemyParticleSystem;
        private float _flyRadius = 1.0f;
        private float _angle;

        public DroneMovementState(Transform transform, Transform playerTransform, DroneStateMachine stateMachine,
            CheckCircleOverlap checker, DroneParticleSystem droneParticleSystem)
        {
            _droneTransform = transform;
            _heroTransform = playerTransform;
            _stateMachine = stateMachine;
            _enemyChecker = checker;
            _enemyParticleSystem = droneParticleSystem;
        }

        public void Enter()
        {
            _enemyParticleSystem.PlayEffect(ParticleEffects.Shield);
        }

        public void Exit()
        {
            _enemyParticleSystem.PlayEffect(ParticleEffects.Dust);
        }

        public void Update()
        {
            if (StateManager.Instance.CurrentGameState == GameStates.Paused)
            {
                return;
            }

            MoveAroundPlayer();

            if (_enemyChecker.CheckCount() > 0)
            {
                _stateMachine.SetWorkState();
            }
        }

        private void MoveAroundPlayer()
        {
            _angle += Time.deltaTime;
            float x = _heroTransform.position.x + _flyRadius * Mathf.Cos(_angle);
            float y = _heroTransform.position.y + _flyRadius * Mathf.Sin(_angle);
            _droneTransform.position = new Vector3(x, y, _droneTransform.position.z);
        }
    }
}