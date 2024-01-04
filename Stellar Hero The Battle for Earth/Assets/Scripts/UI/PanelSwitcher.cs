using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject _effect;
    [SerializeField] private Button _soundButton;

    public void OpenPanel()
    {
        if (_effect != null)
            _effect.SetActive(false);

        _panel.SetActive(true);

        if (_soundButton != null)
            _soundButton.gameObject.SetActive(false);

        StateManager.Instance.SetState(GameStates.Paused);
    }

    public void ClosePanel(bool isEffectEnabled)
    {
        if (_effect != null && isEffectEnabled)
            _effect.SetActive(true);

            _panel.SetActive(false);

        if (_soundButton != null)
            _soundButton.gameObject.SetActive(true);

        StateManager.Instance.SetState(GameStates.Gameplay);
    }

    public void GoToMainMenu()
    {
        int firstSceneIndex = 1;
        SceneManager.LoadScene(firstSceneIndex);
    }
}
