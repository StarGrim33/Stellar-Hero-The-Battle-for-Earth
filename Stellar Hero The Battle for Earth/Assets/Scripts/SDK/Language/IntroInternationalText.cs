using Agava.YandexGames;
using System.Collections;

public class IntroInternationalText : InternationalText
{
    private string _lang = "en";

    protected override void Start()
    {
        StartCoroutine(WaitSDKInitialize());
    }

    private IEnumerator WaitSDKInitialize()
    {
        yield return YandexGamesSdk.Initialize();

        _lang = YandexGamesSdk.Environment.i18n.lang;

        switch (_lang)
        {
            case _enLanguage:
                SetText(_en);
                break;
            case _ruLanguage:
                SetText(_ru);
                break;
            case _trLanguage:
                SetText(_tr);
                break;
            default:
                SetText(_en);
                break;
        }
    }
}
