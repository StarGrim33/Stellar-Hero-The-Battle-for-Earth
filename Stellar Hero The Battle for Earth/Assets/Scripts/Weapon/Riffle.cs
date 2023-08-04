using System.Collections;
using UnityEngine;

public class Riffle : MonoBehaviour, IWeapon
{
    [SerializeField] private Bullet _bulletPrefab;
    private float _shootForce = 200f;

    public void PerformShot(Transform shootPoint)
    {
        var bullet = Instantiate(_bulletPrefab, shootPoint);
        bullet.transform.position = shootPoint.position;
        var rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(-shootPoint.up * _shootForce, ForceMode2D.Impulse);
    }
}
