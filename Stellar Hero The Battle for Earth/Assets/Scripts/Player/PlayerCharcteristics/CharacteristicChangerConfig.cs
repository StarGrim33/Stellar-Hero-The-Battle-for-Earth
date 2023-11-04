using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Source/Updates", fileName = "UpdasteConfig", order = 0)]
public class CharacteristicChangerConfig : ScriptableObject
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _enDescription;
    [SerializeField] private string _ruDescription;
    [SerializeField] private string _trDescription;

    [SerializeField] private List<Changes> _changes;

    [SerializeField] private CharacteristicChangerConfig _nextChanger;

    private const string _enLanguage = "en";
    private const string _ruLanguage = "ru";
    private const string _trLanguage = "tr";

    private string _description;

    public Sprite Icon=> _icon;
    public CharacteristicChangerConfig NextChanger => _nextChanger;

    public string GetDescription()
    {
        string lang = Language.Instance.CurrentLanguage;

        switch (lang)
        {
            case _enLanguage:
                return _enDescription;
            case _ruLanguage:
                return _ruDescription;
            case _trLanguage:
                return _trDescription;
            default:
                return _enDescription;
        }
    }

    public void ChangeCharacteristic()
    {
        foreach(var changes  in _changes)
        {
            PlayerCharacteristics.I.AddValue(changes.Characteristics, changes.Value);
        }
    }
}

