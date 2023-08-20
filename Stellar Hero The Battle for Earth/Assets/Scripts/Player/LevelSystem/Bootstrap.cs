using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private LevelWindow _levelWindow;
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private ExperienceHandler _experienceHandler;

    private void Awake()
    {
        PlayerLevelSystem playerLevelSystem = new PlayerLevelSystem();
        _levelWindow.SetLevelSystem(playerLevelSystem);
        _playerAnimator.SetLevelSystem(playerLevelSystem);
        _experienceHandler.Init(playerLevelSystem);
    }
}
