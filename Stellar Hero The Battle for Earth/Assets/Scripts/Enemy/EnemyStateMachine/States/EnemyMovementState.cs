using UnityEngine;

[RequireComponent(typeof(EnemyUnit), typeof(Rigidbody2D))]
public class EnemyMovementState : State
{
    private float _speed;
    private EnemyUnit _enemyUnit;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _enemyUnit = GetComponent<EnemyUnit>();
        _speed = _enemyUnit.Config.Speed;
    }

    private void Update()
    {
        if (StateManager.Instance.CurrentGameState == GameStates.Paused)
            return;

        if (Target)
        {
            Vector3 direction = (Target.position - transform.position).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
            _rigidbody.rotation = angle;
            transform.position = Vector2.MoveTowards(transform.position, Target.position, _speed * Time.deltaTime);
        }
    }
}
