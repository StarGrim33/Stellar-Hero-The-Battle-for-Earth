using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 _direction;
    private int _damage = 1;
    public float maxDistance = 10f; 

    public void Shot(Vector3 startPoint, Vector3 endPoint, float speed, int damage)
    {
        CalculateDirection(startPoint, endPoint);
        transform.position = startPoint;
        StartCoroutine(ShotCoroutine(speed));
        _damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out IDamageable enemyHealth))
        {
            enemyHealth.TakeDamage(_damage);
        }
    }

    private IEnumerator ShotCoroutine(float speed)
    {
        float distanceTravelled = 0f;

        while (distanceTravelled < maxDistance)
        {
            transform.position += speed * _direction * Time.deltaTime;
            distanceTravelled += speed * Time.deltaTime;
            yield return null;
        }

        gameObject.SetActive(false);
    }

    private void CalculateDirection(Vector3 startPoint, Vector3 endPoint)
    {
        var heading = endPoint - startPoint;
        var distance = heading.magnitude;
        _direction = heading / distance;
    }
}
