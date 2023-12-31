using Agava.YandexGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardOpener : MonoBehaviour, ILeaderboard
{
    [SerializeField] private GameObject _leaderboardPanel;
    [SerializeField] private GameObject _notAuthorizedPanel;
    [SerializeField] private Button _soundButton;
    [SerializeField] private Button _openLeaderboardButton;
    [SerializeField] private Button _authorizeButton;
    [SerializeField] private Button _declineAuthorizeButton;
    [SerializeField] private TMP_Text[] _playersName;
    [SerializeField] private TMP_Text[] _playersScore;
    [SerializeField] private TextOptimizer _textOptimizer;

    public void OpenLeaderboard()
    {
        if (PlayerAccount.IsAuthorized)
        {
            GetLeaderboardEntriesButtonClick();
            _leaderboardPanel.SetActive(true);
            _soundButton.gameObject.SetActive(false);
        }
        else
        {
            _notAuthorizedPanel.gameObject.SetActive(true);
            _soundButton.gameObject.SetActive(false);
        }
    }

    public void GetLeaderboardEntriesButtonClick()
    {
        Agava.YandexGames.Leaderboard.GetEntries(Constants.LeaderboardName, (result) =>
        {
            ClearLeaderboardPanel();

            for (int i = 0; i < result.entries.Length; i++)
            {
                string name = result.entries[i].player.publicName;
                int score = result.entries[i].score;
                int rank = result.entries[i].rank;

                if (string.IsNullOrEmpty(name))
                {
                    if (Language.Instance.CurrentLanguage == Constants.RussianCode)
                        name = Constants.AnonimRu;
                    else if (Language.Instance.CurrentLanguage == Constants.EnglishCode)
                        name = Constants.AnonymousName;
                    else if (Language.Instance.CurrentLanguage == Constants.TurkishCode)
                        name = Constants.AnonymousName;
                    else
                        name = Constants.AnonimRu;
                }

                _playersName[i].text = _textOptimizer.OptimizeText(name);
                _playersScore[i].text = $"{score}";
            }
        });
    }

    public void ClearLeaderboardPanel()
    {
        foreach (var text in _playersName)
            text.text = string.Empty;

        foreach (var text in _playersScore)
            text.text = string.Empty;
    }
}
