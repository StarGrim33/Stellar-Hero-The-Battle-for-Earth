using TMPro;
using UnityEngine;

public class InternationalText : MonoBehaviour
{
    [SerializeField] protected string _en;
    [SerializeField] protected string _ru;
    [SerializeField] protected string _tr;

    protected const string _enLanguage = "en";
    protected const string _ruLanguage = "ru";
    protected const string _trLanguage = "tr";

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

    protected void SetText(string text)
    {
        _text.text = text;
    }
}