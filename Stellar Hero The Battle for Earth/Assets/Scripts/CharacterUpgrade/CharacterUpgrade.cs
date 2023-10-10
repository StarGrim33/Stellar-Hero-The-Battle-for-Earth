using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Source/Units/CharacterUpgrade", fileName = "CharacterUpgrade", order = 0)]
public class CharacterUpgrade : ScriptableObject
{
    //[SerializeField] private BulletParams _params;
    [SerializeField] private BulletTypeSpawner _typeSpawner;

    [Space, Header("View")]
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _description;

    [Space, Header("Branch")]
    [SerializeField] private List<CharacterUpgrade> _nextUpgrates;

    [Space, Header("UpgradeParams")]
    [SerializeField] private int _damageModify = 0; //100,200,300...
    [SerializeField] private float _attackSpeedModify = 0; // 1.1, 1.2, 1.3 ...
    [SerializeField] private float _bulletSpeedModify = 0; // 1.1, 1.2, 1.3 ...
    [SerializeField] private int _count = 1;
    [SerializeField] private int _typePower = 0;
    [Space]
    [SerializeField] private BulletType _type = BulletType.none;
    [SerializeField] private bool _isChangeType = false;

    public Sprite Icon => _icon;
    public string Description => _description;

    public BulletTypeSpawner BulletTypeSpawner => _typeSpawner;
    public int NumberOfBulletTypeSpawner => (int)_typeSpawner;

    public int Count => _count;
    public int TypePower => _typePower;
    public int DamageModify=> _damageModify;
    public float AttackSpeedModify => _attackSpeedModify;
    public float BulletSpeedModify => _bulletSpeedModify;   
    public BulletType Type => _type;
    public bool IsChangeType => _isChangeType;  
    public List<CharacterUpgrade> NextUpgrades => _nextUpgrates;
}
