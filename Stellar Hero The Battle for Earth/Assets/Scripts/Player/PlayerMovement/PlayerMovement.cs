using Assets.Scripts.Components.Checkers;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerUnit))]
public class PlayerMovement : MonoBehaviour, IControllable
{
    [Space]
    [Header("Dash")]
    [SerializeField] private float _duration;
    [SerializeField] private float _distance;
    [SerializeField] private Cooldown _dashCooldown;
    [SerializeField] private CheckCircleOverlap _obstacleChecker;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private ButtonFiller _dushFiller;

    public float CurrentSpeed { get; private set; }

    public Vector3 Direction => _direction;

    public event UnityAction<bool> Dashing;

    private PlayerParticleSystem _particleSystem;
    private Rigidbody2D _rigidBody;
    private Vector2 _direction;
    private PlayerUnit _playerUnit;
    private Vector2 _startDashPostioin;
    private Vector2 _endDashPostioin;
    private float _dashTimer;
    private bool _isDash = false;
    private float _speed;

    private void Awake()
    {
        _particleSystem = GetComponent<PlayerParticleSystem>();
        _playerUnit = GetComponent<PlayerUnit>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _speed = _playerUnit.Config.Speed;
    }

    private void FixedUpdate()
    {
        if (StateManager.Instance.CurrentGameState == GameStates.Paused)
            return;

        if (_isDash)
            Dash();
        else
            Move();
    }

    public void SetDirection(Vector2 direction)
    {
        if (direction.x < 0)
            _sprite.flipX = true;
        else
            _sprite.flipX = false;

        _direction = direction;
    }

    public void Move()
    {
        _rigidBody.velocity = _direction * _speed;
        _particleSystem.PlayEffect();
        CurrentSpeed = _rigidBody.velocity.magnitude;
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
            Dashing?.Invoke(true);
        }
    }

    private void Dash()
    {
        _dushFiller.StartFilled(_dashCooldown.Value);

        _dashTimer += Time.fixedDeltaTime;
        float time = _dashTimer / _duration;
        transform.position = Vector2.Lerp(_startDashPostioin, _endDashPostioin, time);
        _particleSystem.PlayEffect();
        _isDash = time < 1;
        Dashing?.Invoke(false);

        if (_obstacleChecker.CheckCount() > 0)
            _isDash = false;
    }
}
