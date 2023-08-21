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
                RotateWeapon(weapon);
                weapon.PerformShot();
            }
        }
    }

    private void RotateWeapon(Weapon currentTarget)
    {
        var rightAngle = 90f;
        var _directionToTarget = currentTarget.Target - transform.position;
        float angle = Mathf.Atan2(_directionToTarget.y, _directionToTarget.x) * Mathf.Rad2Deg;
        currentTarget.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        if (angle <= rightAngle && angle >= -rightAngle) 
            currentTarget.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        else
            currentTarget.transform.localScale = new Vector3(1.2f, -1.2f, -1.2f);
    }
}