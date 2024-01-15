using Utils;
using UnityEngine;

namespace Weapon
{
    public class EnterTriggerDamage : MonoBehaviour
    {
        [SerializeField] private bool _isTriggerDamage;
        private int _damage = 0;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_isTriggerDamage == false || _damage == 0)
                return;

            if (collision.TryGetComponent<IDamageable>(out IDamageable damageable))
                damageable.TakeDamage(_damage);
        }

        public void OnTriggerDamage() => _isTriggerDamage = true;

        public void OffTriggerDamage() => _isTriggerDamage = false;

        public void SetDamage(int damage) => _damage = damage;
    }
}