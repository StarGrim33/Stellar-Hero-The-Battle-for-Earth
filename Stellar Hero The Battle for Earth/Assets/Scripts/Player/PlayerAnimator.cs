using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private ParticleSystem _levelUpEffect;
    private PlayerLevelSystem _playerLevelSystem;

    private void Update()
    {
        _animator.SetFloat("Speed", _movement.CurrentSpeed);
    }

    public void SetLevelSystem(PlayerLevelSystem playerLevelSystem)
    {
        _playerLevelSystem = playerLevelSystem;
        _playerLevelSystem.OnLevelChanged += OnLevelChanged;
    }

    private void OnLevelChanged()
    {
        _levelUpEffect.Play();
    }
}
