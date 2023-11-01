using Assets.Scripts.Components.Checkers;
using UnityEngine;

public class DroneMovementState : IStateSwitcher
{
    private Transform _droneTransform;
    private Transform _heroTransform;
    private float _flyRadius = 1.0f;
    private float _angle = 0.0f;
    private DroneStateMachine _stateMachine;
    private CheckCircleOverlap _enemyChecker;

    public DroneMovementState(Transform transform, Transform playerTransform, DroneStateMachine stateMachine, CheckCircleOverlap checker)
    {
        _droneTransform = transform;
        _heroTransform = playerTransform;
        _stateMachine = stateMachine;
        _enemyChecker = checker;
    }

    public void Enter()
    {

    }

    public void Exit()
    {

    }

    public void Update()
    {
        if (StateManager.Instance.CurrentGameState == GameStates.Paused)
            return;

        MoveAroundPlayer();

        if (_enemyChecker.CheckCount() > 0)
            _stateMachine.SetWorkState();
    }

    private void MoveAroundPlayer()
    {
        _angle += Time.deltaTime;
        float x = _heroTransform.position.x + _flyRadius * Mathf.Cos(_angle);
        float y = _heroTransform.position.y + _flyRadius * Mathf.Sin(_angle);
        _droneTransform.position = new Vector3(x, y, _droneTransform.position.z);
    }
}
