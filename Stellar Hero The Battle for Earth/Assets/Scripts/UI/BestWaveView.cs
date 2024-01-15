using SDK;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Utils
{
    public class BestWaveView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Button _recordButton;
        private string _language;
        private int _bestWave;

        public int BestWave => _bestWave;

        private void OnEnable()
        {
            _recordButton.onClick.AddListener(UpdateUI);
            UpdateUI();
        }

        private void OnDisable()
        {
            _recordButton.onClick.RemoveListener(UpdateUI);
        }

        private void UpdateUI()
        {
            const int defaultValue = 1;
            _bestWave = Saves.Load(Constants.BestWaveKey, defaultValue);
            _language = Language.Instance.CurrentLanguage;
            _text.text = GetBestWaveText(_language, _bestWave);
        }

        private string GetBestWaveText(string language, int wave)
        {
            string languageCode = GetValidLanguageCode(language);
            string waveTextPrefix = GetWaveTextPrefix(languageCode);
            return waveTextPrefix + wave.ToString();
        }

        private string GetValidLanguageCode(string language)
        {
            return language switch
            {
                Constants.EnglishCode => Constants.EnglishCode,
                Constants.TurkishCode => Constants.TurkishCode,
                _ => Constants.RussianCode,
            };
        }

        private string GetWaveTextPrefix(string languageCode)
        {
            return languageCode switch
            {
                Constants.EnglishCode => Constants.BestWaveTextEn,
                Constants.TurkishCode => Constants.BestWaveTextTr,
                _ => Constants.BestWaveTextRu,
            };
        }
    }
}