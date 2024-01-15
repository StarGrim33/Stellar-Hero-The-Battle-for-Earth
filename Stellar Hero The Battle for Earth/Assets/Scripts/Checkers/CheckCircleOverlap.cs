using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Assets.Scripts.Components.Checkers
{
    public class CheckCircleOverlap : MonoBehaviour
    {
        private const int ColliderSize = 5;

        private readonly Collider2D[] _collidedResult = new Collider2D[ColliderSize];

        [SerializeField] private float _radius;
        [SerializeField] private LayerMask _layer;

        public LayerMask Layer => _layer;

        public List<T> Check<T>() where T : IDamageable
        {
            var result = new List<T>();

            var size = Physics2D.OverlapCircleNonAlloc(transform.position, _radius, _collidedResult, _layer);

            for (int i = 0; i < size; i++)
            {
                if (_collidedResult[i].gameObject.TryGetComponent<T>(out var damageable))
                {
                    result.Add(damageable);
                }
            }

            return result;
        }

        public int CheckCount()
        {
            return Physics2D.OverlapCircleNonAlloc(
                transform.position,
                _radius,
                _collidedResult,
                _layer);
        }

        public void ChangeRadius(float radius)
        {
            _radius = radius;
        }
    }
}