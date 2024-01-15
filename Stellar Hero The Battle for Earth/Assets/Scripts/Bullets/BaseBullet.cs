using Utils;
using System.Collections;
using UnityEngine;

namespace Bullets
{
    public abstract class BaseBullet : MonoBehaviour, IBullet
    {
        protected int Damage = 1;
        [SerializeField] private float maxDistance = 10f;
        private Vector3 _direction;

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<IDamageable>(out IDamageable enemyHealth))
            {
                enemyHealth.TakeDamage(Damage);
            }
        }

        public void Shot(Vector3 startPoint, Vector3 endPoint, float speed, int damage)
        {
            CalculateDirection(startPoint, endPoint);
            transform.position = startPoint;
            StartCoroutine(ShotCoroutine(speed));
            Damage = damage;
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
}
