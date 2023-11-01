using UnityEngine;

public class MobileCharacterController : MonoBehaviour
{
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private AbilityButton _abilityButton;

    private IControllable _iControllable;

    private void Awake()
    {
        _iControllable = GetComponent<IControllable>();
    }

    private void OnEnable()
    {
        _joystick.ChangeDirection += OnGetDirection;
        _abilityButton.Clicked += OnDashOnPerformed;
    }

    private void OnDisable()
    {
        _joystick.ChangeDirection -= OnGetDirection;
        _abilityButton.Clicked -= OnDashOnPerformed;
    }

    private void OnDashOnPerformed()
    {
        _iControllable.TryDash();
    }

    private void OnGetDirection()
    {
        var direction = _joystick.Direction;
        _iControllable.SetDirection(direction);
    }
}
