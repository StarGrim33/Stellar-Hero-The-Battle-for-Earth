using Assets.Scripts.Components.Checkers;
using Assets.Scripts.Utils;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerUnit))]
public class PlayerMovement : MonoBehaviour, IControllable
{
    [SerializeField] private float _speed;

    [Space]
    [Header("Dash")]
    [SerializeField] private float _duration;
    [SerializeField] private float _distance;
    [SerializeField] private Cooldown _dashCooldown;
    [SerializeField] private CheckCircleOverlap _obstacleChecker;

    private Rigidbody2D _rigidBody;
    private Vector2 _direction;
<<<<<<< Updated upstream
    private PlayerUnit _playerUnit;
=======
>>>>>>> Stashed changes
    private Vector2 _startDashPostioin;
    private Vector2 _endDashPostioin;
    private float _dashTimer;
    private bool _isDash = false;

    private void Awake()
    {
        _playerUnit = GetComponent<PlayerUnit>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _speed = _playerUnit.Config.Speed;
    }

    private void FixedUpdate()
    {
        if (_isDash)
            Dash();
        else
            Move();
    }

    public void Move()
    {
        _rigidBody.velocity = _direction * _speed;
    }

<<<<<<< Updated upstream
    public void Dash()
=======
    private void Dash()
>>>>>>> Stashed changes
    {
        _dashTimer += Time.fixedDeltaTime;
        float time = _dashTimer / _duration;
        transform.position = Vector2.Lerp(_startDashPostioin, _endDashPostioin, time);
        _isDash = time >= 1 ? false : true;
<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
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

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }
}
