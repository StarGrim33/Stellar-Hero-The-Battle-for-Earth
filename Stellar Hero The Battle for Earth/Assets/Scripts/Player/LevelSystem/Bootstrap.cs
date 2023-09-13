using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private LevelWindow _levelWindow;
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private ExperienceHandler _experienceHandler;
    [SerializeField] private GameplayMediator _gameplayMediator;
    [SerializeField] private DefeatPanel _defeatPanel;
    [SerializeField] private PlayerHealth _playerHealth;

    private void Awake()
    {
        PlayerLevelSystem playerLevelSystem = new();
        Level level = new(_playerHealth);
        _levelWindow.SetLevelSystem(playerLevelSystem);
        _playerAnimator.SetLevelSystem(playerLevelSystem);
        _experienceHandler.Init(playerLevelSystem);
        _gameplayMediator.Initialize(level);
        _defeatPanel.Initialize(_gameplayMediator);
    }
}
