using Agava.YandexGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardOpener : MonoBehaviour
{
    [SerializeField] private GameObject _leaderboardPanel;
    [SerializeField] private GameObject _notAuthorizedPanel;
    [SerializeField] private Button _openLeaderboardButton;
    [SerializeField] private Button _authorizeButton;
    [SerializeField] private Button _declineAuthorizeButton;
    [SerializeField] private TMP_Text[] _playersName;
    [SerializeField] private TMP_Text[] _playersScore;
    [SerializeField] private int _maxLength;

    public void OnOpenLeaderBoard()
    {
        if (PlayerAccount.IsAuthorized)
        {
            OnGetLeaderboardEntriesButtonClick();
            _leaderboardPanel.SetActive(true);
        }
        else
        {
            _notAuthorizedPanel.gameObject.SetActive(true);
        }
    }

    public void OnGetLeaderboardEntriesButtonClick()
    {
        Agava.YandexGames.Leaderboard.GetEntries(Constants.LeaderboardName, (result) =>
        {
            Debug.Log($"My rank = {result.userRank}");
            ClearLeaderboardPanel();

            for (int i = 0; i < result.entries.Length; i++)
            {
                string name = result.entries[i].player.publicName;
                int score = result.entries[i].score;
                int rank = result.entries[i].rank;

                if (string.IsNullOrEmpty(name))
                {
                    if (Language.Instance.CurrentLanguage == Constants.RussianCode)
                        name = "Аноним";
                    else if (Language.Instance.CurrentLanguage == Constants.EnglishCode)
                        name = Constants.AnonymousName;
                    else if (Language.Instance.CurrentLanguage == Constants.TurkishCode)
                        name = "Anonim";
                    else
                        name = "Аноним";
                }

                _playersName[i].text = TextOprimizer(name);
                _playersScore[i].text = $"{score}";
            }
        });
    }

    private void ClearLeaderboardPanel()
    {
        foreach (var text in _playersName)
        {
            text.text = string.Empty;
        }

        foreach (var text in _playersScore)
        {
            text.text = string.Empty;
        }
    }

    private string TextOprimizer(string name)
    {
        string nameToLower = name.ToLower();

        char[] letters = name.ToCharArray();
        letters[0] = char.ToUpper(letters[0]);

        if (nameToLower.Length > _maxLength)
            return nameToLower[.._maxLength];
        else
            return nameToLower;
    }
}
