using UnityEngine;
using Utils;

public class TutorialPresenter
{
    private TutorialModel _model;
    private TutorialView _view;

    public TutorialPresenter(TutorialView view)
    {
        _model = new TutorialModel();
        _view = view;

        _model.ShowTutorial = PlayerPrefs.GetInt(Constants.TurorialShowed) == 0;

        if (_model.ShowTutorial)
            ShowTutorial();
    }

    public void OnTutorialClick()
    {
        if (_model.CurrentTutorialIndex + 1 < _view.TutorialsCount)
        {
            _view.HideTutorial(_model);
            _model.CurrentTutorialIndex++;
            _view.ShowTutorial(_model);
        }
        else
        {
            _view.HideTutorial(_model);
            PlayerPrefs.SetInt(Constants.TurorialShowed, 1);
            PlayerPrefs.Save();
            _view.EnablePlayerControls();
        }
    }

    private void ShowTutorial()
    {
        _view.ShowTutorial(_model);
    }
}
