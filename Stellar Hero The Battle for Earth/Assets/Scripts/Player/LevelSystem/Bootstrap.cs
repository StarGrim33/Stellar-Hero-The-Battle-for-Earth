using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private LevelWindow _levelWindow;
    [SerializeField] private PlayerAnimator _playerAnimator;

    private void Awake()
    {
        PlayerLevelSystem playerLevelSystem = new PlayerLevelSystem();
        _levelWindow.SetLevelSystem(playerLevelSystem);
        _playerAnimator.SetLevelSystem(playerLevelSystem);
    }
}
