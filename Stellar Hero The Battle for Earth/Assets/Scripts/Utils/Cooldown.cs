﻿using System;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    [Serializable]
    public class Cooldown
    {
        [SerializeField] private float _value;

        public float Value => _value;

        private float _timesUp;

        public void Reset()
        {
            _timesUp = Time.time + _value;
        }

        public bool IsReady() => _timesUp < Time.time;

        public void ChangeCooldownValue(float value) => _value = value;
    }
}