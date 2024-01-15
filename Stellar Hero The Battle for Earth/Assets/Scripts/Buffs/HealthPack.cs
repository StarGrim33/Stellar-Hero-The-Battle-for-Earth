using Player;
using UnityEngine;

namespace Buffs
{
    public class HealthPack : BaseBuff
    {
        [SerializeField] private int _healValue;

        public override void Take(PlayerHealth playerHealth)
        {
            playerHealth.Heal(_healValue);
            Destroy(gameObject);
        }
    }
}