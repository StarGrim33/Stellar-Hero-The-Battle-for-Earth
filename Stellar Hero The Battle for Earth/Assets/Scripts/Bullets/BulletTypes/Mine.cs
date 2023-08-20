using UnityEngine;

public class Mine : Bullet
{
    public override void Shot(Vector3 startPoint, Vector3 targetPoint, float modifySpeed, int modifyDamage, BulletType type = BulletType.none, int typePower =0)
    {
        transform.position = startPoint;

        SetDamage(modifyDamage);
    }
}
