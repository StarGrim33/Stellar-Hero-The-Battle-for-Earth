using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(IControllable))]
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
            _joystick.ChangeDirection += SetDirection;
            _abilityButton.Clicked += Dash;
        }

        private void OnDisable()
        {
            _joystick.ChangeDirection -= SetDirection;
            _abilityButton.Clicked -= Dash;
        }

        private void Dash()
        {
            _iControllable.TryDash();
        }

        private void SetDirection()
        {
            var direction = _joystick.Direction;
            _iControllable.SetDirection(direction);
        }
    }
}