using Assets.Scripts.Components.Checkers;
using System.Collections.Generic;
using UnityEngine;

public class DroneMovementState : IStateSwitcher
{
    private Transform _droneTransform;
    private Transform _heroTransform;
    private float _flyRadius = 1.0f;
    private float _angle = 0.0f;
    private DroneStateMachine _stateMachine;
    private CheckCircleOverlap _enemyChecker;
    private IDamageable _currentTarget;

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
        if (_enemyChecker.CheckCount() < 0)
            return;

        //if (_target.position.x > _droneSprite.position.x)
        //{
        //    _droneSprite.localScale = new Vector3(1f, 1f, 1f);
        //}
        //else
        //{
        //    _droneSprite.localScale = new Vector3(-1f, 1f, 1f);
        //}

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

    private void RotateToTarget()
    {

    }

}
