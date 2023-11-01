using UnityEngine;

public class PanelSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    public void OpenPanel()
    {
        _panel.SetActive(true);
        StateManager.Instance.SetState(GameStates.Paused);
    }

    public void ClosePanel()
    {
        _panel.SetActive(false);
        StateManager.Instance.SetState(GameStates.Gameplay);
    }
}
