using UnityEngine;

namespace Core
{
    public abstract class UnitHealth : MonoBehaviour
    {
        protected float MaxHealth;
        protected float ĐurrenHealth;
        [SerializeField] private Unit _unit;

        protected virtual void OnEnable()
        {
            MaxHealth = _unit.Config.Health;
            ĐurrenHealth = MaxHealth;
        }

        protected virtual void Die()
        {
        }
    }
}