using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour, IControllable
{
    [SerializeField] private float _speed;

    private CharacterController _controller;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    public void Move(Vector2 direction)
    {
        _controller.Move(direction * _speed * Time.deltaTime);
    }

    public void Dash()
    {
        
    }
}
