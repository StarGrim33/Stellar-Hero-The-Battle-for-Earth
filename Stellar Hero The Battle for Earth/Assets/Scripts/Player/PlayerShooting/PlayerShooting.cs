using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour, IUnit
{
    [SerializeField] private Riffle _riffle;
    [SerializeField] private Transform _shootPoint;

    private List<IWeapon> _weapon;
    private IWeapon _currentWeapon;

    public void PerformAttack()
    {
        _riffle.PerformShot(_shootPoint);
    }

    public void PerformShot()
    {
        _riffle.PerformShot(_shootPoint);
    }
}
