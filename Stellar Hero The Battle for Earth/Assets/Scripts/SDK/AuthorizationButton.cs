using Agava.YandexGames;
using UnityEngine;
using UnityEngine.UI;

public class AuthorizationButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Button _soundButton;
    [SerializeField] private GameObject _panel;

    public void OnAuthorizeButtonClick()
    {
        PlayerAccount.Authorize();
    }

    public void OnDeclineAuthorizationButton()
    {
        _panel.gameObject.SetActive(false);
        _soundButton.gameObject.SetActive(true);
    }
}
