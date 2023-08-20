using UnityEngine;

public interface IDamageable
{
    bool IsAlive { get; }

    Transform TargetTransform { get;}

    public void TakeDamage(int damage);
}
