using TMPro;
using UnityEngine;

public class InternationalText : MonoBehaviour
{
    [SerializeField] private string _en;
    [SerializeField] private string _ru;
    [SerializeField] private string _tr;

    private const string _enLanguage = "en";
    private const string _ruLanguage = "ru";
    private const string _trLanguage = "tr";

    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void Start()
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

    private void SetText(string text)
    {
        _text.text = text;
    }
}