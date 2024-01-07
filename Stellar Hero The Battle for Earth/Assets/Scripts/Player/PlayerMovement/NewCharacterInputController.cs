using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(IControllable))]
public class NewCharacterInputController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private IControllable _iControllable;
    private Input _gameInput;
    private Vector2 _mousePosition;

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

    private void OnEnable() => Init();

    private void OnMousePositionPerformed(InputAction.CallbackContext callbackContext)
    {
        _mousePosition = _camera.ScreenToWorldPoint(callbackContext.ReadValue<Vector2>());
    }

    private void OnDisable() => Finalization();

    private void OnDashOnPerformed(InputAction.CallbackContext callbackContext)
    {
        if (StateManager.Instance.CurrentGameState == GameStates.Gameplay)
            _iControllable.TryDash();
    }

    private void OnGetDirection(InputAction.CallbackContext context)
    {
        var direction = context.ReadValue<Vector2>();
        _iControllable.SetDirection(direction);
    }

    private void Init()
    {
        _gameInput.Gameplay.Dash.performed += OnDashOnPerformed;
        _gameInput.Gameplay.Movement.performed += OnGetDirection;
        _gameInput.Gameplay.Movement.canceled += OnGetDirection;
        _gameInput.Gameplay.MousePosition.performed += OnMousePositionPerformed;
    }

    private void Finalization()
    {
        _gameInput.Gameplay.MousePosition.performed -= OnMousePositionPerformed;
        _gameInput.Gameplay.Dash.performed -= OnDashOnPerformed;
        _gameInput.Gameplay.Movement.performed -= OnGetDirection;
        _gameInput.Gameplay.Movement.canceled -= OnGetDirection;
    }
}
