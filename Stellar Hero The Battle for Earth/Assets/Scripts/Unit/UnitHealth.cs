using UnityEngine;

public abstract class UnitHealth : MonoBehaviour
{
    [SerializeField] private Unit _unit;

    protected float MaxHealth;
    protected float ĐurrenHealth;

    protected virtual void OnEnable()
    {
        MaxHealth = _unit.Config.Health;
        ĐurrenHealth = MaxHealth;
    }

    protected virtual void Die() { }
}
