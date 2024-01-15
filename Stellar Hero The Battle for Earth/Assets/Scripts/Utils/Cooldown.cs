using System;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    [Serializable]
    public class Cooldown
    {
        [SerializeField] private float _value;
        private float _timesUp;

        public float Value => _value;

        public void Reset()
        {
            _timesUp = Time.time + _value;
        }

        public bool IsReady() => _timesUp < Time.time;

        public void ChangeCooldownValue(float value) => _value = value;
    }
}