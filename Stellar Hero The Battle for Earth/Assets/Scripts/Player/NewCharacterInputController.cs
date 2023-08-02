using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewCharacterInputController : MonoBehaviour
{
    private IControllable _iControllable;
    private Input _gameInput;

    private void Awake()
    {
        _iControllable = GetComponent<IControllable>();

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
    }

    private void OnDisable()
    {
        _gameInput.Gameplay.Dash.performed -= OnDashOnPerformed;
    }

    private void Update()
    {
        ReadMovement();
    }

    private void OnDashOnPerformed(InputAction.CallbackContext obj)
    {
        _iControllable.Dash();
    }

    private void ReadMovement()
    {
        var inputDirection = _gameInput.Gameplay.Movement.ReadValue<Vector2>();
        var direction = new Vector2(inputDirection.x, inputDirection.y);
        _iControllable.Move(direction);
    }
}
