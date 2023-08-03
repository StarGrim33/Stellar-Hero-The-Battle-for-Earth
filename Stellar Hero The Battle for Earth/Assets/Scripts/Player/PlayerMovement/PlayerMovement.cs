using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerUnit))]
public class PlayerMovement : MonoBehaviour, IControllable
{
    [SerializeField] private float _speed;

    private Rigidbody2D _rigidbody;
    private PlayerUnit _playerUnit;

    private void Awake()
    {
        _playerUnit = GetComponent<PlayerUnit>();
        _speed = _playerUnit.Config.Speed;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 direction)
    {
        _rigidbody.MovePosition(_rigidbody.position + direction * _speed * Time.fixedDeltaTime);
    }

    public void Dash()
    {

    }
}
