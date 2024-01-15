using System.Collections;
using Agava.YandexGames;
using Utils;

namespace SDK
{
    public class IntroInternationalText : InternationalText
    {
        protected override void Start()
        {
            StartCoroutine(WaitSDKInitialize());
        }

        private IEnumerator WaitSDKInitialize()
        {
            yield return YandexGamesSdk.Initialize();

            switch (Language.Instance.CurrentLanguage)
            {
                case (Constants.EnglishCode):
                    SetText(EnglishLanguage);
                    break;

                case (Constants.RussianCode):
                    SetText(RussianLanguage);
                    break;

                case (Constants.TurkishCode):
                    SetText(TurkishLanguage);
                    break;

                default:
                    SetText(RussianLanguage);
                    break;
            }
        }
    }
}