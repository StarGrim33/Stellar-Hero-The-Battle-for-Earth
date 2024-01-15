using UnityEngine;

namespace Utils
{
    public abstract class UnitHealth : MonoBehaviour
    {
        [SerializeField] private Unit _unit;

        public virtual float MaxHealth { get; protected set; }

        public virtual float CurrentHealth { get; protected set; }

        protected virtual void OnEnable()
        {
            MaxHealth = _unit.Config.Health;
            CurrentHealth = MaxHealth;
        }

        protected virtual void Die()
        {
        }
    }
}