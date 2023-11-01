using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance { get; private set; }

    public GameStates CurrentGameState { get; private set; }

    public delegate void GameStateChangeHandler(GameStates newGameState);

    public event GameStateChangeHandler OnGameStateChange;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Debug.LogWarning("Multiple instances of Spawner found. Only one instance should exist.");
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void SetState(GameStates state)
    {
        if (state == CurrentGameState) return;

        CurrentGameState = state;
        OnGameStateChange?.Invoke(state);
    }
}
