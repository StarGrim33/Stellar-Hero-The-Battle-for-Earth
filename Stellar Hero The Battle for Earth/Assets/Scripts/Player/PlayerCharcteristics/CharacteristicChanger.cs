using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacteristicChanger : MonoBehaviour
{
    [SerializeField] private List<Changes> _changes;


    private PlayerCharacteristics _characteristics;

    private void Start()
    {
        _characteristics = PlayerCharacteristics.I;
    }

    public void ChangeCharacteristic()
    {
        foreach(var changes  in _changes)
        {
            _characteristics.AddValue(changes.Characteristics, changes.Value);
        }
    }
}

[Serializable]
public struct Changes
{
    [SerializeField] private Characteristics _characteristics;
    [SerializeField] private float _value;

    public Characteristics Characteristics => _characteristics;
    public float Value => _value;
}
