using UnityEngine;

namespace Utils
{
    public class StateManager : MonoBehaviour
    {
        public static StateManager Instance { get; private set; }

        public bool IsLevelUpPanelShowing { get; set; }

        public GameStates CurrentGameState { get; private set; }

        public delegate void GameStateChangeHandler(GameStates newGameState);

        public event GameStateChangeHandler OnGameStateChange;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
        }

        public void SetState(GameStates state)
        {
            if (state != CurrentGameState)
            {
                CurrentGameState = state;
                OnGameStateChange?.Invoke(state);
            }
        }
    }
}