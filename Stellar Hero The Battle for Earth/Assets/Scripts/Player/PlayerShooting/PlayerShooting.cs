using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weapons;

    [Space, Header("WeaponPosition")]
    [SerializeField] private float _radius;

    private float _circleLength = 360f;

    private void Start()
    {
        SetWeaponPosition();
    }

    private void Update()
    {
        foreach (var weapon in _weapons)
        {
            weapon.TryShoot();
        }
    }

    private void SetWeaponPosition()
    {
        if( _weapons.Count == 0 ) return;

        float angleStep = _circleLength / _weapons.Count;

        for(int i=0; i< _weapons.Count; i++)
        {
            float angle = i*angleStep;

            float x = _radius * Mathf.Cos(angle * Mathf.Deg2Rad);
            float y = _radius * Mathf.Sin(angle * Mathf.Deg2Rad);

            _weapons[i].transform.position = new Vector3(x, y, 0);

            _weapons[i].transform.parent = transform;
        }
    }
}
