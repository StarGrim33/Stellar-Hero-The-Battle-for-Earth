using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class TutorialView : MonoBehaviour
{
    [SerializeField] private List<Button> _tutorials;
    [SerializeField] private PlayerMovement _player;
    [SerializeField] private Button _menuButton;

    private TutorialPresenter _presenter;

    public int TutorialsCount => _tutorials.Count;

    private void Start()
    {
        _presenter = new TutorialPresenter(this);
    }

    public void OnTutorialClick()
    {
        _presenter.OnTutorialClick();
    }

    public void HideTutorial(TutorialModel tutorialModel)
    {
        if (tutorialModel.CurrentTutorialIndex < TutorialsCount)
        {
            _tutorials[tutorialModel.CurrentTutorialIndex].gameObject.SetActive(false);
        }
    }

    public void ShowTutorial(TutorialModel tutorialModel)
    {
        if (tutorialModel.ShowTutorial && tutorialModel.CurrentTutorialIndex < TutorialsCount)
        {
            _menuButton.enabled = false;
            _tutorials[tutorialModel.CurrentTutorialIndex].gameObject.SetActive(true);
            _player.enabled = false;
            Time.timeScale = 0f;
            StateManager.Instance.SetState(GameStates.Paused);
        }
    }

    public void EnablePlayerControls()
    {
        Time.timeScale = 1f;
        StateManager.Instance.SetState(GameStates.Gameplay);
        _menuButton.enabled = true;
        _player.enabled = true;
    }
}
