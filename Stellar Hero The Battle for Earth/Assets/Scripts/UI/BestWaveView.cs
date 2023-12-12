using TMPro;
using UnityEngine;

public class BestWaveView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Spawner _currentWave;
    private string _englishLevelText = "Best wave: ";
    private string _russianLevelText = "Лучшая волна: ";
    private string _turkeyLevelText = "En iyi dalga: ";
    private string _language;

    private void OnEnable()
    {
        Agava.YandexGames.Leaderboard.GetPlayerEntry("Leaderboard", (result) =>
        {
            if (result != null)
            {

                _language = Language.Instance.CurrentLanguage;

                _text.text = _language switch
                {
                    Constants.EnglishCode => _englishLevelText + result.score.ToString(),
                    Constants.RussianCode => _russianLevelText + result.score.ToString(),
                    Constants.TurkishCode => _turkeyLevelText + result.score.ToString(),
                    _ => _russianLevelText + result.score.ToString(),
                };
            }
            else
            {
                _language = Language.Instance.CurrentLanguage;

                _text.text = _language switch
                {
                    Constants.EnglishCode => _englishLevelText + _currentWave.CurrentWaveIndex.ToString(),
                    Constants.RussianCode => _russianLevelText + _currentWave.CurrentWaveIndex.ToString(),
                    Constants.TurkishCode => _turkeyLevelText + _currentWave.CurrentWaveIndex.ToString(),
                    _ => _russianLevelText + _currentWave.CurrentWaveIndex.ToString(),
                };
            }
        });
    }
}

