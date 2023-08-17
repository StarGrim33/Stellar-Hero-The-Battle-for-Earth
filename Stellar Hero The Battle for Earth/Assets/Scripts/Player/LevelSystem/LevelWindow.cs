using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelWindow : MonoBehaviour
{
    [SerializeField] private TMP_Text _leveText;
    [SerializeField] private Image _experienceBarImage;
    [SerializeField] private Button _experienceTestButton;
    private PlayerLevelSystem _playerLevelSystem;

    private void SetExperienceBarSize(float experienceNormalized)
    {
        _experienceBarImage.fillAmount = experienceNormalized;
    }

    private void SetLevelNumber(int number)
    {
        number++;
        _leveText.text = number.ToString();
    }

    public void SetLevelSystem(PlayerLevelSystem levelSystem)
    {
        _playerLevelSystem = levelSystem;

        SetLevelNumber(_playerLevelSystem.Level);
        SetExperienceBarSize(_playerLevelSystem.ExperienceNormalized);
        _playerLevelSystem.OnExperienceChanged += OnExperienceChanged;
        _playerLevelSystem.OnLevelChanged += OnLevelChanged;
    }

    public void AddExp()
    {
        _playerLevelSystem.AddExperience(50);
    }

    private void OnLevelChanged()
    {
        SetLevelNumber(_playerLevelSystem.Level);
    }

    private void OnExperienceChanged()
    {
        SetExperienceBarSize(_playerLevelSystem.ExperienceNormalized);
    }
}
