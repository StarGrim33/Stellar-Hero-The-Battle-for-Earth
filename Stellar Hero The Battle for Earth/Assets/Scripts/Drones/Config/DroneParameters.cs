using System;

[Serializable]
public class DroneParameters
{
    public int Damage { get; private set; } = 15;

    public float Delay { get; private set; } = 2f;

    public float FlyRadius { get; private set; } = 1.0f;
}
