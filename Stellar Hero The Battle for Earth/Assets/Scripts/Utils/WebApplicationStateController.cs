using Agava.WebUtility;
using Music;
using Plugins.Audio.Core;
using UnityEngine;

namespace Utils
{
    public class WebApplicationStateController : MonoBehaviour, IWebApplicationStateController
    {
        [SerializeField] private StateManager _stateManager;
        [SerializeField] private MusicPlayer _backgroundSource;
        [SerializeField] private SourceAudio _gunSource;
        private bool _isAdvShowing;

        private void OnEnable()
        {
            WebApplication.InBackgroundChangeEvent += OnInBackgroundChangeWeb;
        }

        private void OnDisable()
        {
            WebApplication.InBackgroundChangeEvent -= OnInBackgroundChangeWeb;
        }

        public void UnPause(bool isAdvShowing)
        {
            _isAdvShowing = isAdvShowing;

            if (StateManager.Instance.IsLevelUpPanelShowing)
            {
                return;
            }

            if (!isAdvShowing)
            {
                _backgroundSource.UnPause();
                _gunSource?.UnPause();
                _stateManager.SetState(GameStates.Gameplay);
            }
        }

        public void Pause(bool isAdvShowing)
        {
            _isAdvShowing = isAdvShowing;
            _backgroundSource.Pause();
            _gunSource?.Pause();
            _stateManager.SetState(GameStates.Paused);
        }

        private void OnInBackgroundChangeWeb(bool inBackground)
        {
            MuteAudio(inBackground);
            PauseGame(inBackground);
        }

        private void PauseGame(bool inBackground)
        {
            if (StateManager.Instance.IsLevelUpPanelShowing)
            {
                return;
            }

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
                _gunSource?.Pause();
            }
            else if (!inBackground)
            {
                if (!_isAdvShowing)
                {
                    _backgroundSource.UnPause();
                    _gunSource?.UnPause();
                }
            }
        }
    }
}