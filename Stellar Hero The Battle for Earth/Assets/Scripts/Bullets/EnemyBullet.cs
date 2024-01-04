using System.Collections;
using UnityEngine;

public class EnemyBullet : MonoBehaviour, IBullet
{
    [SerializeField] float maxDistance = 10f;
    private Vector3 _direction;
    private int _damage = 0;

    public void Shot(Vector3 startPoint, Vector3 endPoint, float speed, int damage)
    {
        CalculateDirection(startPoint, endPoint);
        transform.position = startPoint;
        StartCoroutine(ShotCoroutine(speed));
        _damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out IDamageable playerHealth))
        {
            if (playerHealth is PlayerHealth)
                playerHealth.TakeDamage(_damage);
        }

        if(collision.TryGetComponent<IBullet>(out IBullet playerBullet))
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator ShotCoroutine(float speed)
    {
        float distanceTravelled = 0f;

        while (distanceTravelled < maxDistance)
        {
            transform.position += speed * Time.deltaTime * _direction;
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
