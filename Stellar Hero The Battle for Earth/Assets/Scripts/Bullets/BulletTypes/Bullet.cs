using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected int _baseDamage = 10;
    [SerializeField] protected float _baseBulletSpeed = 1f;

    protected int _damage = 1;
    protected BulletType _type;
    protected int _typePower;
    private Vector3 _direction;

    public virtual void Shot(Vector3 startPoint, Vector3 targetPoint,
        float modifySpeed = 0f, int modifyDamage = 1, BulletType type = BulletType.none, int typePower = 0)
    {
        _type = type;
        _typePower = typePower;

        CalculateDirection(startPoint, targetPoint);
        transform.position = startPoint;
        StartCoroutine(ShotingCoroutine(_baseBulletSpeed * modifySpeed));

        SetDamage(modifyDamage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth))
        {
            TriggerEnemyAction(collision, enemyHealth);
        }
        else
        {
            TriggerObstacleAction(collision);
        }
    }

    protected virtual void TriggerEffect()
    {
    }

    protected virtual void TriggerEnemyAction(Collider2D collision, EnemyHealth enemyHealth)
    {
        enemyHealth.TakeDamage(_damage);

        switch (_type)
        {
            case BulletType.none:
                DestroyBullet();
                break;
            case BulletType.pierce:
                PierceAction();
                break;
            case BulletType.rebound:
                ReboundAction(collision);
                break;
        }

        TriggerEffect();
    }

    protected virtual void TriggerObstacleAction(Collider2D collision)
    {
        switch (_type)
        {
            case BulletType.rebound:
                ReboundAction(collision);
                break;
            default:
                DestroyBullet();
                break;
        };
    }

    protected virtual void DestroyBullet()
    {
        Destroy(gameObject);
    }

    protected virtual void ReboundAction(Collider2D collision)
    {
        if (_typePower > 0)
        {
            Bounced(collision);
            _typePower--;
        }
        else
            DestroyBullet();
    }

    protected virtual void PierceAction()
    {
        if (_typePower > 0)
            _typePower--;
        else
            DestroyBullet();
    }

    protected virtual IEnumerator ShotingCoroutine(float speed)
    {
        while (true)
        {
            transform.position += _direction * speed * Time.deltaTime;

            yield return null;
        }
    }

    protected virtual void CalculateDirection(Vector3 startPoint, Vector3 endPoint)
    {
        var heading = endPoint - startPoint;
        var distance = heading.magnitude;
        _direction = heading / distance;
    }

    protected void SetDamage(int modifyDamage)
    {
        _damage = (int)(_baseDamage * (1 + modifyDamage / 100f));
    }

    protected void Bounced(Collider2D collision)
    {
        Vector2 normal = collision.transform.position - transform.position;
        _direction = Vector2.Reflect(_direction, normal.normalized);
        _typePower--;
    }
}
