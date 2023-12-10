using Agava.WebUtility;
using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class WebUtilityFixer : MonoBehaviour
{
    [SerializeField] private GameSettings _environment;
    [SerializeField] private StateManager _stateManager;
    [SerializeField] private AudioSource[] _gunSounds;

#if UNITY_WEBGL && !UNITY_EDITOR

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
        foreach (var sound in _gunSounds)
        {
            sound.enabled = inBackground;
            sound.volume = inBackground ? 0f : 1f;
        }
 
        if (inBackground)
            _stateManager.SetState(GameStates.Paused);
        else
            _stateManager.SetState(GameStates.Gameplay);
    }
#endif
}

