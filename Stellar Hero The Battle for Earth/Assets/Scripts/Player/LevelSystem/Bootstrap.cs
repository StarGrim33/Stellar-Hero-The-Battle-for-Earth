using Agava.WebUtility;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private Image _ability;
    [SerializeField] private Image _ability2;
    [SerializeField] private Image _stick;
    [SerializeField] private Image _stick2;

    private void Awake()
    {
        Initialization();
    }

    private void ControllerInitialize()
    {
        if (Device.IsMobile)
        {
            EnableMobileController();
        }
        else
        {
            EnableDesktopController();
        }
    }

    private void EnableMobileController()
    {
        _ability.enabled = true;
        _ability2.enabled = true;
        _stick.enabled = true;
        _stick2.enabled = true;
        _mobileController.enabled = true;
    }

    private void EnableDesktopController()
    {
        _ability.enabled = false;
        _ability2.enabled = false;
        _stick.enabled = false;
        _stick2.enabled = false;
        _desktopController.enabled = true;
    }

    private void Initialization()
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
}
