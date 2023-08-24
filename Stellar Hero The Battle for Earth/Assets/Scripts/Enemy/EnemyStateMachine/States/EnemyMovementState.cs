using UnityEngine;

[RequireComponent(typeof(EnemyUnit))]
public class EnemyMovementState : State
{
    protected Animator _animator;
    private float _speed;
    private EnemyUnit _enemyUnit;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemyUnit = GetComponent<EnemyUnit>();
        _speed = _enemyUnit.Config.Speed;
    }

    private void Update()
    {
        if (StateManager.Instance.CurrentGameState == GameStates.Paused)
            return;

        if (Target != null)
            transform.position = Vector2.MoveTowards(transform.position, Target.TargetTransform.position, _speed * Time.deltaTime);
    }
}
