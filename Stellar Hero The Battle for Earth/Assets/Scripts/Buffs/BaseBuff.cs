using Player;
using UnityEngine;

namespace Buffs
{
    public abstract class BaseBuff : MonoBehaviour
    {
        public float Timer { get; set; }

        public abstract void Take(PlayerHealth playerHealth);
    }
}