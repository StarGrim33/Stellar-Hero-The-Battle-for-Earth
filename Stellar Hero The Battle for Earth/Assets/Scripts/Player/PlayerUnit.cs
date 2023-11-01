

public class PlayerUnit : Unit
{
    private PlayerHealth _health;

    public PlayerHealth PlayerHealth => _health;

    private void Start()
    {
        _health = GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if(collision.TryGetComponent<Buff>(out Buff buff))
        {
            buff.Take(_health);
        }
    }
}
