using UnityEngine;

public class CircleBulletSpawner : BulletSpawner
{
    private float fullAngle = 360f;

    protected override void Shot(BulletType type, int typePower = 0)
    {
        int circleCount = _count;
        float angleStep = fullAngle / circleCount;
        float firstShotAngle = Random.Range(0, fullAngle);

        for (int i = 0; i < _count; i++)
        {
            var bullet = Instantiate(_tamplate);

            CalculateShotTarget(i - 1, firstShotAngle, angleStep);

            bullet.Shot(gameObject.transform.position, _shotTarget,
                _params.BulletSpeedModify, _params.DamageModify, type, typePower);
        }
    }

    private void CalculateShotTarget(int number, float firstShotAngle, float angleStep)
    {
        float angle = firstShotAngle + number * angleStep;

        float x = Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = Mathf.Sin(angle * Mathf.Deg2Rad);

        _shotTarget = transform.position + new Vector3(x, y, 0f);
    }
}
