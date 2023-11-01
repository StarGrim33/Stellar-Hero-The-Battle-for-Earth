using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Transform _transform;

    private void Start()
    {
        transform.rotation = Quaternion.identity;
    }

    private void Update()
    {
        if (StateManager.Instance.CurrentGameState == GameStates.Paused)
            return;

        foreach (var weapon in _weapons)
        {
            if(weapon.Target != null)
            {
                weapon.PerformShot();
            }
        }
    }
}
