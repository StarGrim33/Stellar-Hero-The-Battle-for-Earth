using UnityEngine;

namespace Utils
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class DroneSpriteRotator : MonoBehaviour
    {
        private SpriteRenderer _droneSprite;
        private DroneAttackState _droneAttackState;

        private void Start()
        {
            _droneSprite = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            RotateToTarget();
        }

        public void Init(DroneAttackState attackState)
        {
            _droneAttackState = attackState;
        }

        private void RotateToTarget()
        {
            if (_droneSprite != null && _droneAttackState.CurrenTarget != null)
            {
                Transform target = _droneAttackState.CurrenTarget;
                _droneSprite.flipX = target.position.x <= transform.position.x;
            }
        }
    }
}