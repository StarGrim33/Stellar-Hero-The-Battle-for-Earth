using UnityEngine;

namespace Bullets
{
    public class PlayerBullet : BaseBullet
    {
        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<IDamageable>(out IDamageable enemyHealth))
            {
                enemyHealth.TakeDamage(Damage);
            }
        }
    }
}
