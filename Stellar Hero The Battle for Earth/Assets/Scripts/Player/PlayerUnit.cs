

using UnityEngine;

public class PlayerUnit : Unit
{
    private PlayerHealth _health;
    private Rigidbody2D _rigidbody;

    public PlayerHealth PlayerHealth => _health;

    private void Awake()
    {
        _health = GetComponent<PlayerHealth>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _health.PlayerDead += PlayerDead;
    }

    private void PlayerDead()
    {
        _rigidbody.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if(collision.TryGetComponent<Buff>(out Buff buff))
        {
            buff.Take(_health);
        }
    }
}
