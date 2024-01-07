using UnityEngine;

public abstract class UnitHealth : MonoBehaviour
{
    [SerializeField] private Unit _unit;

    protected float MaxHealth;
    protected float ÑurrenHealth;

    protected virtual void OnEnable()
    {
        MaxHealth = _unit.Config.Health;
        ÑurrenHealth = MaxHealth;
    }

    protected virtual void Die() { }
}
