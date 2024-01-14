using Buffs;
using Core;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerHealth))]
    public class PlayerUnit : Unit
    {
        private PlayerHealth _health;

        public PlayerHealth PlayerHealth => _health;

        private void Start()
        {
            _health = GetComponent<PlayerHealth>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<BaseBuff>(out BaseBuff buff))
            {
                buff.Take(_health);
            }
        }
    }
}