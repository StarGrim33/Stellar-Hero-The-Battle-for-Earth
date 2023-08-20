using UnityEngine;

public class ExperienceHandler : MonoBehaviour
{
    private PlayerLevelSystem _playerLevelSystem;

    public void Init(PlayerLevelSystem playerLevelSystem)
    {
        _playerLevelSystem = playerLevelSystem;
    }

    public void AddExperience(int amount)
    {
        _playerLevelSystem.AddExperience(amount);
    }
}
