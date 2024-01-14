using Agava.YandexGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace SDK
{
    public class LeaderboardOpener : MonoBehaviour, ILeaderboard
    {
        [SerializeField] private GameObject _leaderboardPanel;
        [SerializeField] private GameObject _notAuthorizedPanel;
        [SerializeField] private Button _soundButton;
        [SerializeField] private TMP_Text[] _playersName;
        [SerializeField] private TMP_Text[] _playersScore;
        [SerializeField] private TextOptimizer _textOptimizer;

        public void OpenLeaderboard()
        {
            switch (PlayerAccount.IsAuthorized)
            {
                case true:
                    ShowLeaderboard();
                    break;

                case false:
                    Authorize();
                    break;
            }
        }

        public void OnGetLeaderboardEntriesButtonClick()
        {
            Leaderboard.GetEntries(Constants.LeaderboardName, (result) =>
            {
                ClearLeaderboardPanel();

                for (int i = 0; i < result.entries.Length; i++)
                {
                    string name = result.entries[i].player.publicName;
                    int score = result.entries[i].score;
                    int rank = result.entries[i].rank;

                    if (string.IsNullOrEmpty(name))
                    {
                        name = Language.Instance.CurrentLanguage switch
                        {
                            Constants.RussianCode => Constants.AnonimRu,
                            Constants.EnglishCode => Constants.AnonymousName,
                            Constants.TurkishCode => Constants.AnonymousName,
                            _ => Constants.AnonimRu,
                        };
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

        private void Authorize()
        {
            _notAuthorizedPanel.gameObject.SetActive(true);
            _soundButton.gameObject.SetActive(false);
        }

        private void ShowLeaderboard()
        {
            OnGetLeaderboardEntriesButtonClick();
            _leaderboardPanel.SetActive(true);
            _soundButton.gameObject.SetActive(false);
        }
    }
}