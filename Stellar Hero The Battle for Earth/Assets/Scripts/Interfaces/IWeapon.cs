using UnityEngine.Events;

public interface IWeapon
{
    public void PerformShot();

    int CurrentAmmo { get; }

    int MaxAmmo { get; }
}
