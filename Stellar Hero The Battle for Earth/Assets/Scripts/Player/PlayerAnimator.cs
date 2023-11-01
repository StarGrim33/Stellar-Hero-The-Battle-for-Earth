using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerMovement _movement;

    private void OnEnable()
    {
        _movement.Dashing += Dashing;
    }

    private void Dashing(bool dash)
    {
        _animator.SetBool("IsDashing", dash);
    }

    private void Update()
    {
        _animator.SetFloat(Constants.Speed, _movement.CurrentSpeed);
    }
}
