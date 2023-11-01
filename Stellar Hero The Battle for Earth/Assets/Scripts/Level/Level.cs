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

    private void OnPlayerDead()
    {
        OnDefeat();
    }

    public void Start()
    {

    }

    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
    }

    public void OnDefeat()
    {
        StateManager.Instance.SetState(GameStates.Paused);
        Defeat?.Invoke();
    }
}
