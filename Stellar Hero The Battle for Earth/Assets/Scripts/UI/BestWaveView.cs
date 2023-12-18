using TMPro;
using UnityEngine;

public class BestWaveView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Spawner _currentWave;
    private string _englishLevelText = "Best wave: ";
    private string _russianLevelText = "������ �����: ";
    private string _turkeyLevelText = "En iyi dalga: ";
    private string _language;

    private void Awake()
    {
        _currentWave.WaveChanged += OnWaveChanged;
    }

    private void OnWaveChanged(int value)
    {
        value.ToString();
    }

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
                int index = _currentWave.CurrentWaveIndex + 1;

                _text.text = _language switch
                {
                    Constants.EnglishCode => _englishLevelText + index,
                    Constants.RussianCode => _russianLevelText + index,
                    Constants.TurkishCode => _turkeyLevelText + index,
                    _ => _russianLevelText + index,
                };
            }
        });

        _language = Language.Instance.CurrentLanguage;

        int index = _currentWave.CurrentWaveIndex + 1; 

        _text.text = _language switch
        {
            Constants.EnglishCode => _englishLevelText + index,
            Constants.RussianCode => _russianLevelText + index,
            Constants.TurkishCode => _turkeyLevelText + index,
            _ => _russianLevelText + index,
        };
    }
}

