using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject _hitEffect;

    private int _damage = 40;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void AddForce(Vector2 direction, float force)
    {
        _rigidbody.AddForce(direction * force, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject effect = Instantiate(_hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.5f);

        gameObject.SetActive(false);

        if (collision.TryGetComponent<IDamageable>(out IDamageable component))
        {
            component.TakeDamage(_damage);
        }
    }
}
