using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class NewCharacterInputController : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private PlayerShooting _playerShooting;
    private IControllable _iControllable;
    private Input _gameInput;
    private Vector2 _mousePosition;
    private Rigidbody2D _rigidbody;
    
    private void Awake()
    {
        _playerShooting = GetComponent<PlayerShooting>();
        _iControllable = GetComponent<IControllable>();
        _rigidbody = GetComponent<Rigidbody2D>();

        if (_iControllable == null)
        {
            throw new Exception("There is no IControllable component");
        }

        _gameInput = new Input();
        _gameInput.Enable();
    }

    private void OnEnable()
    {
        _gameInput.Gameplay.Dash.performed += OnDashOnPerformed;
        _gameInput.Gameplay.MousePosition.performed += OnMousePositionPerformed;
        _gameInput.Gameplay.Attack.performed += OnAttackPerformed;
    }


    private void OnDisable()
    {
        _gameInput.Gameplay.MousePosition.performed -= OnMousePositionPerformed;
        _gameInput.Gameplay.Dash.performed -= OnDashOnPerformed;
        _gameInput.Gameplay.Attack.performed -= OnAttackPerformed;
    }

    private void OnMousePositionPerformed(InputAction.CallbackContext callbackContext)
    {
        _mousePosition = _camera.ScreenToWorldPoint(callbackContext.ReadValue<Vector2>());
    }

    private void Update()
    {
        ReadMovement();
        ReadRotation();
    }

    private void OnDashOnPerformed(InputAction.CallbackContext callbackContext)
    {
        _iControllable.Dash();
    }

    private void OnAttackPerformed(InputAction.CallbackContext callbackContext)
    {
        _playerShooting.PerformShot();
    }

    private void ReadMovement()
    {
        var inputDirection = _gameInput.Gameplay.Movement.ReadValue<Vector2>();
        var direction = new Vector2(inputDirection.x, inputDirection.y);
        _iControllable.Move(direction);
    }

    private void ReadRotation()
    {
        Vector2 facingRotation = _mousePosition - _rigidbody.position;
        float angle = Mathf.Atan2(facingRotation.y, facingRotation.x) * Mathf.Rad2Deg + 90f;
        _rigidbody.MoveRotation(angle);
    }
}
