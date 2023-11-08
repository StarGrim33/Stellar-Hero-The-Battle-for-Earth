using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelWindow : MonoBehaviour
{
    [SerializeField] private UpdateCharacteristicWindow _updateCharacteristicWindow;
    [Space]
    [SerializeField] private TMP_Text _leveText;
    [SerializeField] private Image _experienceBarImage;
    private PlayerLevelSystem _playerLevelSystem;
    private string _englishLevelText = "Level: ";
    private string _russianLevelText = "Уровень: ";
    private string _turkeyLevelText = "Seviye: ";
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
            Constants.EnglishCode => _englishLevelText + number.ToString(),
            Constants.RussianCode => _russianLevelText + number.ToString(),
            Constants.TurkishCode => _turkeyLevelText + number.ToString(),
            _ => _russianLevelText + number.ToString(),
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
