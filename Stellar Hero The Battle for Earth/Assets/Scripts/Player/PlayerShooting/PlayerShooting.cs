using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Transform _transform;
    [SerializeField] private Sprite _image;
    [Space, Header("WeaponPosition")]
    [SerializeField] private float _radius;

    private float _circleLength = 360f;

    private void Start()
    {
        transform.rotation = Quaternion.identity;

        //SetWeaponPosition();
    }

    private void Update()
    {
        foreach (var weapon in _weapons)
        {
            //if (weapon.Target != null)
            //    LookAtTarget(weapon);
            //else
            //    transform.rotation = Quaternion.identity;
            RotateWeapon(weapon);
            weapon.PerformShot();
        }
    }

    //private void SetWeaponPosition()
    //{
    //    if (_weapons.Count == 0) return;

    //    float angleStep = _circleLength / _weapons.Count;

    //    for (int i = 0; i < _weapons.Count; i++)
    //    {
    //        float angle = i * angleStep;

    //        float x = _radius * Mathf.Cos(angle * Mathf.Deg2Rad);
    //        float y = _radius * Mathf.Sin(angle * Mathf.Deg2Rad);

    //        _weapons[i].transform.parent = transform;
    //    }
    //}

    private void RotateWeapon(Weapon currentTarget)
    {
        var _directionToTarget = currentTarget.Target - transform.position;
        float angle = Mathf.Atan2(_directionToTarget.y, _directionToTarget.x) * Mathf.Rad2Deg;
        currentTarget.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        if (angle < 90 && angle > -90) 
        {
            currentTarget.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }
        else
        {
            currentTarget.transform.localScale = new Vector3(0.8f, -0.8f, -0.8f);
        }
    }
}
