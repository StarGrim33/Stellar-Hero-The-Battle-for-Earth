using Agava.WebUtility;
using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class WebUtilityFixer : MonoBehaviour
{
    [SerializeField] private GameSettings _environment;
    [SerializeField] private StateManager _stateManager;

    private void Awake()
    {
        if (Device.IsMobile || IsMobile())
        {
            if (_environment != null)
            {
                _environment.IsMobile = true;
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
        Application.focusChanged += OnInBackgroundChange;
        WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;
    }

    private void OnDisable()
    {
        Application.focusChanged -= OnInBackgroundChange;
        WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;
    }

    private void OnInBackgroundChange(bool inBackground)
    {
        AudioListener.pause = inBackground;
        AudioListener.volume = inBackground ? 0f : 1f;

        if (inBackground)
            _stateManager.SetState(GameStates.Paused);
        else
            _stateManager.SetState(GameStates.Gameplay);
    }
}
