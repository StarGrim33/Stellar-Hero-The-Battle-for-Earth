using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerMovement _movement;

    private void Update()
    {
        _animator.SetFloat(Constants.Speed, _movement.CurrentSpeed);
    }
}
