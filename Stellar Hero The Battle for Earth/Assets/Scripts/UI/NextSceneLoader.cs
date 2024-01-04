using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneLoader : MonoBehaviour
{
    [SerializeField] private int _levelIndex;
    [SerializeField] private GameObject _panel;

    public void LoadScene()
    {
        SceneManager.LoadScene(_levelIndex);
        _panel.SetActive(false);
        StateManager.Instance.SetState(GameStates.Gameplay);
    }
}
