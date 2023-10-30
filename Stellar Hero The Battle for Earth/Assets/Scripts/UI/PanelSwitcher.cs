using UnityEngine;

public class PanelSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject _effect;

    public void OpenPanel()
    {
        if(_effect != null)
            _effect.SetActive(false);

        _panel.SetActive(true);
        StateManager.Instance.SetState(GameStates.Paused);
    }

    public void ClosePanel()
    {
        if (_effect != null)
            _effect.SetActive(true);

        _panel.SetActive(false);
        StateManager.Instance.SetState(GameStates.Gameplay);
    }
}
