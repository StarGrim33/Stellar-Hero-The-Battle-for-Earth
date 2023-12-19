using Agava.WebUtility;
using System;
using UnityEngine;

public class WebUtilityFixer : MonoBehaviour
{
    [SerializeField] private GameSettings _environment;
    [SerializeField] private StateManager _stateManager;
    [SerializeField] private MusicPlayer _backgroundSource;
    [SerializeField] private AudioSource _gunSource;
    private bool _isAdvShowing = false;

    //#if UNITY_WEBGL && !UNITY_EDITOR
    private void Awake()
    {
        if (Device.IsMobile || IsMobile())
        {
            if (_environment != null)
            {
                //_environment.IsMobile = true;
                Debug.Log("Mobile is initialized");
            }
        }
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
            _gunSource.Pause();
            Debug.Log("Paused");
        }
        else if(!inBackground)
        {
            if(!_isAdvShowing)
            {
                Debug.Log("UnPaused");
                _backgroundSource.UnPause();
                _gunSource.UnPause();
            }
            else
            {
                Debug.Log("Still ADV");
            }
        }
    }

    public void UnPause(bool isAdvShowing)
    {
        _isAdvShowing = isAdvShowing;

        if (StateManager.Instance.IsLevelUpPanelShowing)
            return;

        if (!isAdvShowing)
        {
            Debug.Log($"UnPaused + {_isAdvShowing}");
            _backgroundSource.UnPause();
            _gunSource.UnPause();
            _stateManager.SetState(GameStates.Gameplay);
        }
    }

    public void Pause(bool isAdvShowing)
    {
        _isAdvShowing = isAdvShowing;

        Debug.Log($"UnPaused + {_isAdvShowing}");
        _backgroundSource.Pause();
        _gunSource.Pause();
        _stateManager.SetState(GameStates.Paused);
    }
    //#endif
}

