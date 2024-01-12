using TMPro;
using UnityEngine;
using Utils;

public class InternationalText : MonoBehaviour
{
    [SerializeField] protected string _en;
    [SerializeField] protected string _ru;
    [SerializeField] protected string _tr;

    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    protected virtual void Start()
    {
        string lang = Language.Instance.CurrentLanguage;

        switch (lang)
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

    protected void SetText(string text)
    {
        _text.text = text;
    }
}