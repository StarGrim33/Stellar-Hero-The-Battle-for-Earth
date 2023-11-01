using System;
using UnityEngine;

[Serializable]
internal class Changes
    {
    [SerializeField] private Characteristics _characteristics;
    [SerializeField] private float _value;

    public Characteristics Characteristics => _characteristics;
    public float Value => _value;
}



