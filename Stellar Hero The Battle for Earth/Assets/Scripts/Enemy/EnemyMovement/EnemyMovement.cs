using UnityEngine;

[RequireComponent(typeof(EnemyUnit), typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    private Transform _target;
    private float _speed;
    private EnemyUnit _enemyUnit;
    private Rigidbody2D _rigidbody;
    private Vector2 _moveDirection;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _enemyUnit = GetComponent<EnemyUnit>();
        _speed = _enemyUnit.Config.Speed;
    }

    private void Update()
    {
        if (_target)
        {
            Vector3 direction = (_target.position - transform.position).normalized;
            _moveDirection = direction;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
            _rigidbody.rotation = angle;
        }
    }

    private void FixedUpdate()
    {
        if (_target)
            _rigidbody.velocity = new Vector2(_moveDirection.x, _moveDirection.y) * _speed;
    }

    public void SetTarget(Transform target)
    {
        if(target != null)
            _target = target;
    }
}
