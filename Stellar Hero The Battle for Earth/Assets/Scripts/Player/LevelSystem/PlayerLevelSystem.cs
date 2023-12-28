using System;
using System.Linq;

public class PlayerLevelSystem
{
    private readonly int[] _experienceLevel;

    private int _minLevelExp = 100;
    private int _maxLevelExp = 341;
    private int _level;
    private int _experience;

    public event Action OnExperienceChanged;

    public event Action OnLevelChanged;

    public int Level => _level;

    public float ExperienceNormalized => (float)_experience / ExperienceToNextLevel;

    public int ExperienceToNextLevel => _experienceLevel[_level];

    public bool IsMaxLevel => _level == _experienceLevel.Length - 1;

    public PlayerLevelSystem()
    {
        _level = 0;
        _experience = 0;
        _experienceLevel = Enumerable.Range(_minLevelExp, _maxLevelExp).ToArray();
    }

    public void AddExperience(int amount)
    {
        if(IsMaxLevel == false)
        {
            _experience += amount;

            while (_experience >= ExperienceToNextLevel)
            {
                _experience -= ExperienceToNextLevel;
                _level++;
                OnLevelChanged?.Invoke();
            }

            OnExperienceChanged?.Invoke();
        }
    }
}
