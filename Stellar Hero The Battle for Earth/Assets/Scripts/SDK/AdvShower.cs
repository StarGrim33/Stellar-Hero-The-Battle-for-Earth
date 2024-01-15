using System.Collections;
using Agava.YandexGames;
using Player;
using Plugins.Audio.Core;
using TMPro;
using UnityEngine;
using Utils;

namespace SDK
{
    public class AdvShower : MonoBehaviour, IAdShow
    {
        private readonly float _second = 1f;

        [SerializeField] private TMP_Text _countDown;
        [SerializeField] private WebApplicationStateController _fixer;
        [SerializeField] private SourceAudio[] _audioSources;
        [SerializeField] private PlayerHealth _player;
        private WaitForSeconds _delay;

        private void Start()
        {
            _delay = new WaitForSeconds(_second);
        }

        public void ShowAdv()
        {
            switch (Language.Instance.CurrentLanguage)
            {
                case (Constants.RussianCode):
                    StartCoroutine(ShowAdWithCountdown(Constants.RuAdvText));
                    break;

                case (Constants.EnglishCode):
                    StartCoroutine(ShowAdWithCountdown(Constants.EnAdvText));
                    break;

                case (Constants.TurkishCode):
                    StartCoroutine(ShowAdWithCountdown(Constants.TrAdvText));
                    break;

                default:
                    StartCoroutine(ShowAdWithCountdown(Constants.RuAdvText));
                    break;
            }
        }

        public void ShowVideoAd()
        {
            VideoAd.Show(onOpenCallback: OnOpenCallback, onCloseCallback: OnCloseCallback);
        }

        public void IsAdvEnded(bool isAdvEnded)
        {
            if (isAdvEnded)
            {
                _fixer.UnPause(false);

                foreach (var source in _audioSources)
                    source.UnPause();

                StateManager.Instance.SetState(GameStates.Gameplay);
            }
        }

        public void OnCloseCallback()
        {
            _fixer.UnPause(false);

            foreach (var source in _audioSources)
                source.UnPause();

            _player.Revive();
            StateManager.Instance.SetState(GameStates.Gameplay);
        }

        public void OnOpenCallback()
        {
            foreach (var source in _audioSources)
                source.Pause();

            _fixer.Pause(true);
            StateManager.Instance.SetState(GameStates.Paused);
        }

        private IEnumerator ShowAdWithCountdown(string text)
        {
            int countdown = 3;

            while (countdown > 0)
            {
                _countDown.text = text + countdown.ToString();
                yield return _delay;
                countdown--;
                _countDown.text = text + countdown.ToString();
            }

            yield return _delay;

            InterstitialAd.Show(onOpenCallback: OnOpenCallback, onCloseCallback: IsAdvEnded);
            _countDown.text = string.Empty;
        }
    }
}