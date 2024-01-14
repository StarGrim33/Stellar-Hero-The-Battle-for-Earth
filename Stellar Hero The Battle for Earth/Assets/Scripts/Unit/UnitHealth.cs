using UnityEngine;

namespace Core
{
    public abstract class UnitHealth : MonoBehaviour
    {
        protected float MaxHealth;
        protected float �urrenHealth;
        [SerializeField] private Unit _unit;

        protected virtual void OnEnable()
        {
            MaxHealth = _unit.Config.Health;
            �urrenHealth = MaxHealth;
        }

        protected virtual void Die()
        {
        }
    }
}