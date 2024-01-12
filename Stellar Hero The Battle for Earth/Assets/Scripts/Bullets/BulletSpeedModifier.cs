using UnityEngine;

namespace Bullets
{
    public class BulletSpeedModifier : MonoBehaviour
    {
        private readonly float _baseBulletSpeed = 3f;
        [SerializeField] private float _bulletSpeedModify = 0;

        public float GetBulletSpeed()
        {
            return _baseBulletSpeed * (1f + _bulletSpeedModify);
        }
    }
}
