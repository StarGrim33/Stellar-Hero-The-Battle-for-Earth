using Bullets;
using UnityEngine;

namespace Utils
{
    public class TrailInstantiator : MonoBehaviour
    {
        [SerializeField] private BulletTrail _trailPrefab;
        [SerializeField] private Transform _gunPoint;

        public void Instantiate(Vector3 targetPosition)
        {
            var trail = Instantiate(_trailPrefab, _gunPoint.transform.position, Quaternion.identity);
            trail.SetTargetPosition(targetPosition);
        }
    }
}