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
        StartCoroutine(DisablefterDelay(5f));

        _damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<IDamageable>(out IDamageable enemyHealth))
        {
            enemyHealth.TakeDamage(_damage);
        }
    }

    private IEnumerator ShotCoroutine(float speed)
    {
        while (true)
        {
            transform.position += speed * Time.deltaTime * _direction;

            yield return null;
        }        
    }

    private void CalculateDirection(Vector3 startPoint, Vector3 endPoint)
    {
        var heading = endPoint - startPoint;
        var distance = heading.magnitude;
        _direction = heading / distance;
    }

    private IEnumerator DisablefterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
