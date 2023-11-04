using Agava.YandexGames;
using UnityEngine;
using UnityEngine.UI;

public class AuthorizationButton : MonoBehaviour
{
    [SerializeField] private Button _button;

    public void OnAuthorizeButtonClick()
    {
        PlayerAccount.Authorize();
    }

    public void OnDeclineAuthorizationButton(GameObject panel)
    {
        panel.gameObject.SetActive(false);
    }
}
