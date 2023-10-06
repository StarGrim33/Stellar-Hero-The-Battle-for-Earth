using System;
using UnityEngine;

public class GameplayMediator : MonoBehaviour
{
    [SerializeField] private DefeatPanel _defeatPanel;

    private Level _level;

    private void OnDisable()
    {
        _level.Defeat -= OnDefeat;
    }

    public void Initialize(Level level)
    {
        if (level is null)
        {
            throw new ArgumentNullException(nameof(level));
        }

        _level = level;
        _level.Defeat += OnDefeat;
    }

    private void OnDefeat() => _defeatPanel.Show();

    public void RestartLevel()
    {
        _defeatPanel.Hide();
        _level.Restart();
    }
}
