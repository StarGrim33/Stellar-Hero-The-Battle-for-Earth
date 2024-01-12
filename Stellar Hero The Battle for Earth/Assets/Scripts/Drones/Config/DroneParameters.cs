using System;

namespace Drone
{
    [Serializable]
    public class DroneParameters
    {
        public int Damage { get; } = 15;

        public float Delay { get; } = 2f;

        public float FlyRadius { get; } = 1.0f;
    }
}