using Assets.Scripts.Components.Checkers;
using Assets.Scripts.Utils;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour, IControllable
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Transform _spriteImage;

    [Space]
    [Header("Dash")]
    [SerializeField] private float _duration;
    [SerializeField] private float _distance;
    [SerializeField] private Cooldown _dashCooldown;
    [SerializeField] private CheckCircleOverlap _obstacleChecker;

    private Rigidbody2D _rigidBody;
    private Vector2 _direction;

    private Vector2 _startDashPostioin;
    private Vector2 _endDashPostioin;
    private float _dashTimer;
    private bool _isDash = false;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_isDash)
            Dash();
        else
            Move();
    }

    private void Move()
    {
        _rigidBody.velocity = _direction * _speed;
    }

    public void Dash()
    {
        _dashTimer += Time.fixedDeltaTime;
        float t = _dashTimer / _duration;
        transform.position = Vector2.Lerp(_startDashPostioin, _endDashPostioin, t);
        _isDash = t >= 1 ? false : true;
        if (_obstacleChecker.CheckCount() > 0)
            _isDash = false;
    }

    public void TryDash()
    {
        if (_dashCooldown.IsReady())
        {
            _dashCooldown.Reset();
            _startDashPostioin = transform.position;
            var directionDash = _direction == Vector2.zero ? Vector2.down : _direction;
            _endDashPostioin = _startDashPostioin + directionDash * _distance;
            _dashTimer = 0;
            _isDash = true;
        }
    }

    void IControllable.Move()
    {
        throw new System.NotImplementedException();
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }
}
