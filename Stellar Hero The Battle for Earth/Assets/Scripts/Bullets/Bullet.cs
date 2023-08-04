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
}
