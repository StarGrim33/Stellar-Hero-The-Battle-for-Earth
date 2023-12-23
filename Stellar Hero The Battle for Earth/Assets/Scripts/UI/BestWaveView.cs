using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BestWaveView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Spawner _currentWave;
    [SerializeField] private Button _recordButton;

    public int BestWave => _bestWave;

    private string _englishLevelText = "Best wave: ";
    private string _russianLevelText = "Лучшая волна: ";
    private string _turkeyLevelText = "En iyi dalga: ";
    private string _language;
    private int _bestWave;

    private void OnEnable()
    {
        _recordButton.onClick.AddListener(UpdateUI);
        UpdateUI();
    }

    private void OnDisable()
    {
        _recordButton.onClick.RemoveListener(UpdateUI);
    }

    private void GetPlayerScore()
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
        });

       SetBestWave();
    }

    private void SetBestWave()
    {
        _bestWave = Saves.Load("BestWave", 1);
        Debug.Log($"Лучший счет получен - {_bestWave}");

        _language = Language.Instance.CurrentLanguage;

        _text.text = _language switch
        {
            Constants.EnglishCode => _englishLevelText + _bestWave.ToString(),
            Constants.RussianCode => _russianLevelText + _bestWave.ToString(),
            Constants.TurkishCode => _turkeyLevelText + _bestWave.ToString(),
            _ => _russianLevelText + _bestWave.ToString(),
        };
    }

    private void UpdateUI()
    {
        SetBestWave();

        GetPlayerScore();
    }
}

