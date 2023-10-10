using UnityEngine;

public class BulletParams : MonoBehaviour
{
    [SerializeField] private int _damageModify = 0;
    [SerializeField] private float _attackSpeedModify = 1;
    [SerializeField] private float _bulletSpeedModify = 1;

    public int DamageModify => _damageModify;
    public float AttackSpeedModify => _attackSpeedModify;
    public float BulletSpeedModify => _bulletSpeedModify;

    private void Start()
    {
        _damageModify = 0;
        _attackSpeedModify = 1;
        _bulletSpeedModify = 1;
    }

    public void Upgrade(int damageModify, float attackSpeedModify =0, float bulletSpeedModify = 0)
    {
        _damageModify += damageModify;
        _attackSpeedModify += attackSpeedModify;
        _bulletSpeedModify += bulletSpeedModify; 
    }
}
