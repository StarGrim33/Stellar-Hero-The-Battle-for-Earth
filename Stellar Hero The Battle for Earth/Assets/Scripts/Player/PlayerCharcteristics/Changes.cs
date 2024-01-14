using System;
using UnityEngine;

namespace Player
{
    [Serializable]
    internal class Changes
    {
        [SerializeField] private Characteristics _characteristics;
        [SerializeField] private float _value;

        public Characteristics Characteristics => _characteristics;

        public float Value => _value;
    }
}