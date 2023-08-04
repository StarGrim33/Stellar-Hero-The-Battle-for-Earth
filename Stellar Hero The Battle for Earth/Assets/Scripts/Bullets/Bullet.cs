using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject _hitEffect;
    private int _damage = 40;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out IDamageable component))
        {
            GameObject effect = Instantiate(_hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.5f);
            component.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}
