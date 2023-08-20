using System.Collections;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] protected BulletParams _params;

    [Header("AttackCooldown")]
    [SerializeField] private float _baseAttackCooldown = 1f;
    [SerializeField] private float _attackCooldownKoefficient = 1f;

    [Space, Header("Template")]
    [SerializeField] protected Bullet _tamplate;
    [SerializeField] protected int _count = 1;
    [SerializeField] protected BulletType _type = BulletType.none;
    [SerializeField] protected int _typePower = 0;

    protected float _shotDelay;
    protected Vector3 _shotTarget;

    private void Start()
    {
        SetDefaultParams();

        _shotDelay = _baseAttackCooldown / (_attackCooldownKoefficient * _params.AttackSpeedModify);
        StartCoroutine(Spawn());
    }

    public void SetType(BulletType type) => _type = type;
    protected void SetCount(int value) => _count = value;
    protected void SetTypePower(int value) => _typePower = value;

    public void Upgrade(int count, int typePower)

    {
        _count += count;
        _typePower += typePower;
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            Shot(_type, _typePower);

            yield return new WaitForSeconds(_shotDelay);
        }
    }

    protected virtual void SetDefaultParams()
    {
        _count = 0;
        _type = BulletType.none;
        _typePower = 0;
    }

    protected virtual void Shot(BulletType type = BulletType.none, int typePower = 0)
    {

    }
}
