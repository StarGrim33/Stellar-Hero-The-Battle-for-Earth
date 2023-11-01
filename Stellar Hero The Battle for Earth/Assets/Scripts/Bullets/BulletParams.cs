using UnityEngine;

public class BulletParams : MonoBehaviour
{
    [SerializeField] private int _count = 1;
    [SerializeField] private float _dispertion = 10;

    [SerializeField] private float _damageModify = 0;
    [SerializeField] private float _attackSpeedModify = 0;
    [SerializeField] private float _bulletSpeedModify = 0;

    private int _baseDamage = 10;
    private float _baseAttackSpeed = 1f;
    private float _baseCooldownAttack = 1f;
    private float _baseBulletSpeed = 3f;

    public int Damage => (int)(_baseDamage * (1f + _damageModify));

    public float AttackCooldown => _baseCooldownAttack/(_baseAttackSpeed*(1f+ _attackSpeedModify));

    public float BulletSpeed =>  _baseBulletSpeed * (1f + _bulletSpeedModify);

    public int Count => _count;

    public float StepDispertion => _dispertion/2;
}
