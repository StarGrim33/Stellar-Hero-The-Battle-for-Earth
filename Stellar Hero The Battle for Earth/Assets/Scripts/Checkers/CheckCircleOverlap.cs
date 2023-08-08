using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Components.Checkers
{
    public class CheckCircleOverlap : MonoBehaviour
    {
        [SerializeField] private float _radius;
        [SerializeField] private LayerMask _layer;

        private readonly Collider2D[] _collidedResult = new Collider2D[5];

        public List<GameObject> Check()
        {
            var result = new List<GameObject>();

            var size = Physics2D.OverlapCircleNonAlloc(
                transform.position,
                _radius,
                _collidedResult,
                _layer);

            for (int i = 0; i < size; i++)
            {
                if (_collidedResult[i].gameObject.TryGetComponent<EnemyUnit>(out EnemyUnit unit)) 
                {
                    result.Add(_collidedResult[i].gameObject);
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