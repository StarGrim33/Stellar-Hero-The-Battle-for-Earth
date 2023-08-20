using UnityEngine;

public class CharacterUpgrade : MonoBehaviour
{
    [SerializeField] private BulletParams _params;
    [SerializeField] private BulletSpawner _spawner;

    [Space, Header("UpgradeParams")]
    [SerializeField] private int _damageModify = 0;
    [SerializeField] private float _attackSpeedModify = 0;
    [SerializeField] private float _bulletSpeedModify = 0;
    [SerializeField] private int _count = 1;
    [SerializeField] private int _typePower = 0;
    [Space]
    [SerializeField] private BulletType _type = BulletType.none;
    [SerializeField] private bool _isChangeType = false;

    public void Upgrade()
    {
        _params.Upgrade(_damageModify, _attackSpeedModify, _bulletSpeedModify);
        _spawner.Upgrade(_count, _typePower);

        if (_isChangeType)
            _spawner.SetType(_type);
    }
}
