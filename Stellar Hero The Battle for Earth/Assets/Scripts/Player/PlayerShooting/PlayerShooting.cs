using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weapons;

    private void Update()
    {
        foreach (var weapon in _weapons)
        {
            weapon.TryShoot();
        }
    }
}
