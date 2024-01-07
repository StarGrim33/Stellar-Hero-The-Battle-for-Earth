using UnityEngine;

public abstract class UnitHealth : MonoBehaviour
{
    [SerializeField] private Unit _unit;

    protected float MaxHealth;
    protected float �urrenHealth;

    protected virtual void OnEnable()
    {
        MaxHealth = _unit.Config.Health;
        �urrenHealth = MaxHealth;
    }

    protected virtual void Die() { }
}
