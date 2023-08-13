using System;
using UnityEngine;

public class Riffle : MonoBehaviour
{
    [SerializeField] private BulletSpawner _bulletPrefab;
    [SerializeField] private float _shootForce = 200f;

    public void PerformShot(Transform shootPoint)
    {
        var bullet = Instantiate(_bulletPrefab, transform);
        bullet.transform.position = shootPoint.position;
        var rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(-shootPoint.up * _shootForce, ForceMode2D.Impulse);
    }

}
