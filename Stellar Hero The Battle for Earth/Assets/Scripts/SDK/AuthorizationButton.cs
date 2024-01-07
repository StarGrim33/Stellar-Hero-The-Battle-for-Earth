using Agava.YandexGames;
using UnityEngine;
using UnityEngine.UI;

public class AuthorizationButton : MonoBehaviour, IAuthorization
{
    [SerializeField] private Button _button;
    [SerializeField] private Button _soundButton;
    [SerializeField] private GameObject _panel;

    public void Authorize()
    {
        PlayerAccount.Authorize();
    }

    public void OnAuthorizeButtonClick()
    {
        Authorize();
    }

    public void OnDeclineAuthorizationButton()
    {
        _panel.gameObject.SetActive(false);
        _soundButton.gameObject.SetActive(true);
    }
}
