using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Source/Updates", fileName = "UpdasteConfig", order = 0)]
public class CharacteristicChangerConfig : ScriptableObject
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _description;

    [SerializeField] private List<Changes> _changes;

    public Sprite Icon=> _icon;
    public string Description => _description;

    public void ChangeCharacteristic()
    {
        foreach(var changes  in _changes)
        {
            PlayerCharacteristics.I.AddValue(changes.Characteristics, changes.Value);
        }
    }
}

