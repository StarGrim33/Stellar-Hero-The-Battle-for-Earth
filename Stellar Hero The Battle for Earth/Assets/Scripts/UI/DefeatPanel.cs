using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DefeatPanel : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _advRestartButton;
    [SerializeField] private Button _menuButton;
    [SerializeField] private AdvShower _advShower;
    [SerializeField] private WebUtilityFixer _fixer;
    private GameplayMediator _mediator;

    private void OnEnable()
    {
        _restartButton.gameObject.SetActive(false);
        _advRestartButton.gameObject.SetActive(false);
        _restartButton.onClick.AddListener(OnRestartClick);
        _advRestartButton.onClick.AddListener(OnAdvRestartClick);
        StartCoroutine(ShowAdvButton());
    }

    private void OnDisable()
    {
        _advRestartButton.onClick.RemoveListener(OnAdvRestartClick);
        _restartButton.onClick.RemoveListener(OnRestartClick);
    }

    public void Initialize(GameplayMediator mediator)
    {
        _mediator = mediator;
    }

    public void Show()
    {
        _menuButton.enabled = false;
        _advShower.ShowAdv();
        gameObject.SetActive(true);
        _advRestartButton.enabled = true;
    }

    public void Hide()
    {
        _menuButton.enabled = true;
        gameObject.SetActive(false);
    }

    public void OnRestartClick() => _mediator.RestartLevel();

    public void OnAdvRestartClick()
    {
        gameObject.SetActive(false);
        _menuButton.enabled = true;
        _advShower.ShowVideoAd();
    }

    private IEnumerator ShowAdvButton()
    {
        int showDelay = 5;
        var waitForSeconds = new WaitForSeconds(showDelay);

        if(_advRestartButton.IsActive() == false)
        {
            yield return waitForSeconds;
            _advRestartButton.gameObject.SetActive(true);
            _restartButton.gameObject.SetActive(true);
        }
    }
}
