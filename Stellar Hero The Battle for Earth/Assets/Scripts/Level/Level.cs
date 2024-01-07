using System;
using UnityEngine.SceneManagement;

public class Level
{
    private readonly PlayerHealth _health;

    public Level(PlayerHealth playerHealth)
    {
        _health = playerHealth;
        _health.PlayerDead += OnPlayerDead;
    }

    ~Level() 
    { 
        _health.PlayerDead -= OnPlayerDead;
    }

    public event Action Defeat;

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

    private void OnPlayerDead() => OnDefeat();
}
