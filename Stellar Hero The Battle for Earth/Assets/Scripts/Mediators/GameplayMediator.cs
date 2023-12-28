using System;
using UnityEngine;
using UnityEngine.UI;

public class GameplayMediator : MonoBehaviour
{
    [SerializeField] private DefeatPanel _defeatPanel;
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private Rigidbody2D _playerUnit;
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

    public void RestartLevel()
    {
        _defeatPanel.Hide();
        _level.Restart();
    }

    private void OnDefeat()
    {
        _playerUnit.velocity = Vector3.zero;
        _defeatPanel.Show();
    }
}
