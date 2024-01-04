using Agava.YandexGames;
using System.Collections;

public class IntroInternationalText : InternationalText
{
    private string _lang;

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
            case (Constants.EnglishCode):
                SetText(_en);
                break;
            case (Constants.RussianCode):
                SetText(_ru);
                break;
            case (Constants.TurkishCode):
                SetText(_tr);
                break;
            default:
                SetText(_en);
                break;
        }
    }
}
