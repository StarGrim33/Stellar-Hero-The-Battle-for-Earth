using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerMovement _movement;

    private void OnEnable() => _movement.Dashing += Dash;

    private void Dash(bool dash) => _animator.SetBool(Constants.DashState, dash);

    private void Update() => _animator.SetFloat(Constants.Speed, _movement.CurrentSpeed);
}
