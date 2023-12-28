using System;
using UnityEngine.SceneManagement;

public class Level
{
    private PlayerHealth _health;

    public event Action Defeat;

    public Level(PlayerHealth playerHealth)
    {
        _health = playerHealth;
        _health.PlayerDead += OnPlayerDead;
    }

    ~Level() 
    { 
        _health.PlayerDead -= OnPlayerDead;
    }

    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
        StateManager.Instance.SetState(GameStates.Gameplay);
    }

    public void OnDefeat()
    {
        StateManager.Instance.SetState(GameStates.Paused);
        Defeat?.Invoke();
    }

    private void OnPlayerDead()
    {
        OnDefeat();
    }
}
