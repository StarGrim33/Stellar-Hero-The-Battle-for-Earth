using TMPro;
using UnityEngine;
using Utils;

namespace SDK
{
    public class InternationalText : MonoBehaviour
    {
        [SerializeField] protected string EnglishLanguage;
        [SerializeField] protected string RussianLanguage;
        [SerializeField] protected string TurkishLanguage;
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        protected virtual void Start()
        {
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
                    SetText(EnglishLanguage);
                    break;
            }
        }

        protected void SetText(string text)
        {
            _text.text = text;
        }
    }
}