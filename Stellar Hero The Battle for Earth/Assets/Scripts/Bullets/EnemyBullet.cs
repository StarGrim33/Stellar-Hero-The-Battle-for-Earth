using UnityEngine;

namespace Bullets
{
    public class EnemyBullet : BaseBullet
    {
        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<IDamageable>(out IDamageable playerHealth))
            {
                if (playerHealth is PlayerHealth)
                {
                    playerHealth.TakeDamage(Damage);
                }
            }

            if (collision.TryGetComponent<IBullet>(out _))
            {
                Destroy(gameObject);
            }
        }
    }
}
