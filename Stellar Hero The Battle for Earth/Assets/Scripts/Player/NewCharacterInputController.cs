using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

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
        _gameInput.Gameplay.Movement.performed += OnGetDirection;
        _gameInput.Gameplay.Movement.canceled += OnGetDirection;
    }

    private void OnDisable()
    {
        _gameInput.Gameplay.Dash.performed -= OnDashOnPerformed;
        _gameInput.Gameplay.Movement.performed -= OnGetDirection;
        _gameInput.Gameplay.Movement.canceled -= OnGetDirection;
    }


    private void OnDashOnPerformed(InputAction.CallbackContext obj)
    {
        _iControllable.TryDash();
    }

    private void OnGetDirection(InputAction.CallbackContext context)
    {
        var direction = context.ReadValue<Vector2>();
        _iControllable.SetDirection(direction);
    }
}
