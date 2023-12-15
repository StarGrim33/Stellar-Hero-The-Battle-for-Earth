using Agava.YandexGames;
using System.Collections;
using TMPro;
using UnityEngine;

public class AdvShower : MonoBehaviour
{
    [SerializeField] private TMP_Text _countDown;
    [SerializeField] private WebUtilityFixer _fixer;
    [SerializeField] private AudioSource[] _audioSources;
    [SerializeField] private PlayerHealth _player;
    private readonly string _ruText = "Показ рекламы через ";
    private readonly string _enText = "Adv in ";
    private readonly string _trText = "Reklam yoluyla ";

    public void ShowAdv()
    {
        string language = Language.Instance.CurrentLanguage;

        switch (language)
        {
            case ("ru"):
                StartCoroutine(ShowAdWithCountdown(_ruText));
                break;

            case ("en"):
                StartCoroutine(ShowAdWithCountdown(_enText));
                break;

            case ("tr"):
                StartCoroutine(ShowAdWithCountdown(_trText));
                break;

            default:
                StartCoroutine(ShowAdWithCountdown(_ruText));
                break;
        }
    }

    public void ShowVideoAd()
    {
        VideoAd.Show(onOpenCallback: Pause, onCloseCallback: IsRewardedAdvEnded);
    }

    private IEnumerator ShowAdWithCountdown(string text)
    {
        int countdown = 3;

        while (countdown > 0)
        {
            _countDown.text = text + countdown.ToString();
            yield return new WaitForSeconds(1);
            countdown--;
            _countDown.text = text + countdown.ToString();
        }

        yield return new WaitForSeconds(1);

        InterstitialAd.Show(onOpenCallback: Pause, onCloseCallback: IsAdvEnded);
        _countDown.text = string.Empty;
    }

    private void IsAdvEnded(bool isAdvEnded)
    {
        if (isAdvEnded)
        {
            _fixer.UnPause(false);

            foreach (var source in _audioSources)
            {
                source.UnPause();
            }
        }
    }
    private void IsRewardedAdvEnded()
    {
        _fixer.UnPause(false);

        foreach (var source in _audioSources)
        {
            source.UnPause();
        }

        _player.Revive();
        StateManager.Instance.SetState(GameStates.Gameplay);
    }

    private void Pause()
    {
        foreach (var source in _audioSources)
        {
            source.Pause();
        }

        _fixer.Pause(true);
    }
}
