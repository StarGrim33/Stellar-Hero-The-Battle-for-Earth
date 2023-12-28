using Agava.WebUtility;
using Plugins.Audio.Core;
using UnityEngine;

public class WebUtilityFixer : MonoBehaviour
{
    [SerializeField] private StateManager _stateManager;
    [SerializeField] private MusicPlayer _backgroundSource;
    [SerializeField] private SourceAudio _gunSource;
    private bool _isAdvShowing = false;

    private void Awake()
    {
        if (Device.IsMobile || IsMobile())
            Debug.Log("Mobile is initialized");
    }

    private bool IsMobile()
    {
        int mobileScreenWidthThreshold = 800;
        int screenWidth = Screen.width;
        return screenWidth <= mobileScreenWidthThreshold;
    }

    private void OnEnable()
    {
        WebApplication.InBackgroundChangeEvent += OnInBackgroundChangeWeb;
    }

    private void OnDisable()
    {
        WebApplication.InBackgroundChangeEvent -= OnInBackgroundChangeWeb;
    }

    private void OnInBackgroundChangeWeb(bool inBackground)
    {
        MuteAudio(inBackground);
        PauseGame(inBackground);
    }

    public void UnPause(bool isAdvShowing)
    {
        _isAdvShowing = isAdvShowing;

        if (StateManager.Instance.IsLevelUpPanelShowing)
            return;

        if (!isAdvShowing)
        {
            _backgroundSource.UnPause();

            if (_gunSource != null)
            {
                _gunSource.UnPause();
            }

            _stateManager.SetState(GameStates.Gameplay);
        }
    }

    public void Pause(bool isAdvShowing)
    {
        _isAdvShowing = isAdvShowing;
        _backgroundSource.Pause();

        if (_gunSource != null)
        {
            _gunSource.Pause();
        }

        _stateManager.SetState(GameStates.Paused);
    }

    private void PauseGame(bool inBackground)
    {
        if (StateManager.Instance.IsLevelUpPanelShowing)
            return;

        if (inBackground)
        {
            _stateManager.SetState(GameStates.Paused);
            Time.timeScale = 0;
        }
        else
        {
            _stateManager.SetState(GameStates.Gameplay);
            Time.timeScale = 1;
        }
    }

    private void MuteAudio(bool inBackground)
    {
        if (inBackground)
        {
            _backgroundSource.Pause();

            if (_gunSource != null)
            {
                _gunSource.Pause();
            }
        }
        else if (!inBackground)
        {
            if (!_isAdvShowing)
            {
                _backgroundSource.UnPause();

                if (_gunSource != null)
                {
                    _gunSource.UnPause();
                }
            }
        }
    }
}

