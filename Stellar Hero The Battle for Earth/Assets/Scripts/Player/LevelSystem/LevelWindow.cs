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

    private void SetExperienceBarSize(float experienceNormalized)
    {
        _experienceBarImage.fillAmount = experienceNormalized;
    }

    private void SetLevelNumber(int number)
    {
        number++;
        _leveText.text = _englishLevelText + number.ToString();
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
