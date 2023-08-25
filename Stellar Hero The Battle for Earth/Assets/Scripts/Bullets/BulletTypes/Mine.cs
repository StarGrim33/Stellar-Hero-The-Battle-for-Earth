using Assets.Scripts.Components.Checkers;
using UnityEngine;

public class Mine : Bullet
{
    [SerializeField] private CheckCircleOverlap _exploseArea;

    public override void Shot(Vector3 startPoint, Vector3 targetPoint, float modifySpeed, int modifyDamage, BulletType type = BulletType.none, int typePower =0)
    {
        transform.position = startPoint;

        SetDamage(modifyDamage);
    }

    private void Explose()
    {
        var targets = _exploseArea.Check();
        foreach (var target in targets)
        {
            if(target.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth))
            {
                enemyHealth.TakeDamage(_damage);
            }
        }
    }
}
