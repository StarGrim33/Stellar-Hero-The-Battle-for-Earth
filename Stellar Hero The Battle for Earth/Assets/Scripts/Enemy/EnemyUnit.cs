using UnityEngine;

public class EnemyUnit : Unit, IDamageable
{
    public void TakeDamage(int damage)
    {
        Debug.Log($"Damage + {damage}");
    }
}