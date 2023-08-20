using UnityEngine;

public class SimpleBulletSpawner : BulletSpawner
{
    [SerializeField] private CrossHair _crosshair;
    [SerializeField] private float _stepDispertion = 10;

    private GameObject _currentTarget;

    protected override void Shot(BulletType type, int typePower = 0)
    {
        _currentTarget = _crosshair.Target;

        if (_currentTarget == null) return;

        for (int i = 0; i < _count; i++)
        {
            var bullet = Instantiate(_tamplate);

            CalculateShotTarget(i - 1);

            bullet.Shot(gameObject.transform.position, _shotTarget, _params.BulletSpeedModify, _params.DamageModify, type, typePower);
        }
    }

    private void CalculateShotTarget(int number)
    {
        int sideModifier = number % 2 == 0 ? -1 : 1;
        var dispertion = _stepDispertion * number * sideModifier;
        Quaternion deviation = Quaternion.Euler(dispertion, dispertion, 0f);
        _shotTarget = deviation * _currentTarget.transform.position;
    }

    protected override void SetDefaultParams()
    {
        _count = 1;
        _type = BulletType.none;
        _typePower = 0;
    }
}
