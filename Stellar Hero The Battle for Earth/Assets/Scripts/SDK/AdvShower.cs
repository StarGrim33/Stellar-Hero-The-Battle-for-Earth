using Agava.YandexGames;
using Player;
using Plugins.Audio.Core;
using System.Collections;
using TMPro;
using UnityEngine;
using Utils;

public class AdvShower : MonoBehaviour
{
    [SerializeField] private TMP_Text _countDown;
    [SerializeField] private WebUtilityFixer _fixer;
    [SerializeField] private SourceAudio[] _audioSources;
    [SerializeField] private PlayerHealth _player;

    public void ShowAdv()
    {
        string language = Language.Instance.CurrentLanguage;

        switch (language)
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
        VideoAd.Show(onOpenCallback: Pause, onCloseCallback: IsRewardedAdvEnded);
    }

    private IEnumerator ShowAdWithCountdown(string text)
    {
        int second = 1;
        var waitForSeconds = new WaitForSeconds(second);
        int countdown = 3;

        while (countdown > 0)
        {
            _countDown.text = text + countdown.ToString();
            yield return waitForSeconds;
            countdown--;
            _countDown.text = text + countdown.ToString();
        }

        yield return waitForSeconds;

        InterstitialAd.Show(onOpenCallback: Pause, onCloseCallback: IsAdvEnded);
        _countDown.text = string.Empty;
    }

    private void IsAdvEnded(bool isAdvEnded)
    {
        if (isAdvEnded)
        {
            _fixer.UnPause(false);

            foreach (var source in _audioSources)
                source.UnPause();

            StateManager.Instance.SetState(GameStates.Gameplay);
        }
    }

    private void IsRewardedAdvEnded()
    {
        _fixer.UnPause(false);

        foreach (var source in _audioSources)
            source.UnPause();

        _player.Revive();
        StateManager.Instance.SetState(GameStates.Gameplay);
    }

    private void Pause()
    {
        foreach (var source in _audioSources)
            source.Pause();

        _fixer.Pause(true);
        StateManager.Instance.SetState(GameStates.Paused);
    }
}
