public class MineSpawner : BulletSpawner
{
    protected override void Shot(BulletType type, int typePower = 0)
    {
        if (_count > 0)
        {
            var bullet = Instantiate(_tamplate);

            bullet.Shot(gameObject.transform.position, gameObject.transform.position, 0f, _params.DamageModify);
        }
    }
}

