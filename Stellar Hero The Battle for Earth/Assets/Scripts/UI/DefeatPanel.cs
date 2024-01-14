using System.Collections;
using Core;
using UnityEngine;
using UnityEngine.UI;

namespace SDK
{
    public class DefeatPanel : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _advRestartButton;
        [SerializeField] private Button _menuButton;
        [SerializeField] private AdvShower _advShower;
        private GameplayMediator _mediator;
        private WaitForSeconds _waitForSeconds;
        private float _delay = 5f;

        private void OnEnable()
        {
            _restartButton.gameObject.SetActive(false);
            _advRestartButton.gameObject.SetActive(false);
            _restartButton.onClick.AddListener(OnRestartClick);
            _advRestartButton.onClick.AddListener(OnAdvRestartClick);
            StartCoroutine(ShowAdvButton());
            _waitForSeconds = new WaitForSeconds(_delay);
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

        public void OnRestartClick()
        {
            _mediator.RestartLevel();
        }

        public void OnAdvRestartClick()
        {
            gameObject.SetActive(false);
            _menuButton.enabled = true;
            _advShower.ShowVideoAd();
        }

        private IEnumerator ShowAdvButton()
        {
            if (!_advRestartButton.IsActive())
            {
                yield return _waitForSeconds;
                _advRestartButton.gameObject.SetActive(true);
                _restartButton.gameObject.SetActive(true);
            }
        }
    }
}