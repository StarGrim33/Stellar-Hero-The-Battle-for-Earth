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
    [SerializeField] private Spawner _spawner;
    [SerializeField] private CurrentWaveView _currentWaveView;
    [SerializeField] private NewCharacterInputController _desktopController;
    [SerializeField] private MobileCharacterController _mobileController;
    [SerializeField] private GameSettings _gameSettings;

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
        _currentWaveView.Init(_spawner);
        ControllerInitialize();
    }

    private void ControllerInitialize()
    {
        if(_gameSettings != null && _mobileController != null && _desktopController != null)
        {
            if(_gameSettings.IsMobile)
            {
                _mobileController.enabled = true;
                _mobileController.enabled = false;
            }
            else
            {
                _mobileController.enabled = false;
                _mobileController.enabled = true;
            }
        }
    }
}
