using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject _hitEffect;
    private int _damage = 40;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<IDamageable>(out IDamageable component))
        {
            component.TakeDamage(_damage);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(_hitEffect, transform.position, Quaternion.identity);
        Destroy(effect);
        Destroy(gameObject);
    }
}
