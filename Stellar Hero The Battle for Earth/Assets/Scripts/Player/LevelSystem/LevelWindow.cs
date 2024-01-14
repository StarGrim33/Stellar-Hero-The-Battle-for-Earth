using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Core
{
    public class LevelWindow : MonoBehaviour
    {
        [SerializeField] private UpdateCharacteristicWindow _updateCharacteristicWindow;
        [SerializeField] private TMP_Text _leveText;
        [SerializeField] private Image _experienceBarImage;
        private PlayerLevelSystem _playerLevelSystem;
        private string _language;

        private void Awake()
        {
            _language = Language.Instance.CurrentLanguage;
        }

        private void SetExperienceBarSize(float experienceNormalized)
        {
            _experienceBarImage.fillAmount = experienceNormalized;
        }

        private void SetLevelNumber(int number)
        {
            number++;

            _leveText.text = _language switch
            {
                Constants.EnglishCode => Constants.EnglishLevelText + number.ToString(),
                Constants.RussianCode => Constants.RussianLevelText + number.ToString(),
                Constants.TurkishCode => Constants.TurkeyLevelText + number.ToString(),
                _ => Constants.RussianLevelText + number.ToString(),
            };
        }

        public void SetLevelSystem(PlayerLevelSystem levelSystem)
        {
            _playerLevelSystem = levelSystem;
            SetLevelNumber(_playerLevelSystem.Level);
            SetExperienceBarSize(_playerLevelSystem.ExperienceNormalized);
            _playerLevelSystem.OnExperienceChanged += OnExperienceChanged;
            _playerLevelSystem.OnLevelChanged += OnLevelChanged;
        }

        private void OnLevelChanged()
        {
            SetLevelNumber(_playerLevelSystem.Level);
            _updateCharacteristicWindow.gameObject.SetActive(true);
        }

        private void OnExperienceChanged()
        {
            SetExperienceBarSize(_playerLevelSystem.ExperienceNormalized);
        }
    }
}