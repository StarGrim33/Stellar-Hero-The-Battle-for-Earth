using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 _direction;
    private int _damage = 1;

    public void Shot(Vector3 startPoint, Vector3 endPoint, float speed, int damage)
    {
        CalculateDirection(startPoint, endPoint);
        transform.position = startPoint;
        StartCoroutine(ShotCoroutine(speed));

        _damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth))
        {
            enemyHealth.TakeDamage(_damage);
        }
        OnTriggerAction();
        
    }

    private void OnTriggerAction()
    {
        Destroy(gameObject);
    }

    private IEnumerator ShotCoroutine(float speed)
    {
        while (true)
        {
            transform.position += _direction * speed * Time.deltaTime;

            yield return null;
        }        
    }
    private void CalculateDirection(Vector3 startPoint, Vector3 endPoint)
    {
        var heading = endPoint - startPoint;
        var distance = heading.magnitude;
        _direction = heading / distance;
    }
}
