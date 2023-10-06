using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private LevelWindow _levelWindow;
    [SerializeField] private PlayerParticleSystem _particleSystemPlayer;
    [SerializeField] private ExperienceHandler _experienceHandler;
    [SerializeField] private GameplayMediator _gameplayMediator;
    [SerializeField] private DefeatPanel _defeatPanel;
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private Pistol _weapon;
    [SerializeField] private AmmoView _ammoView;

    private void Awake()
    {
        PlayerLevelSystem playerLevelSystem = new();
        Level level = new(_playerHealth);
        _levelWindow.SetLevelSystem(playerLevelSystem);
        _particleSystemPlayer.SetLevelSystem(playerLevelSystem);
        _experienceHandler.Init(playerLevelSystem);
        _gameplayMediator.Initialize(level);
        _defeatPanel.Initialize(_gameplayMediator);
        _ammoView.Init(_weapon);
    }
}
