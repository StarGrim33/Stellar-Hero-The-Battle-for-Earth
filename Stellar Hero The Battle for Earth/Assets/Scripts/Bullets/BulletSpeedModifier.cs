using UnityEngine;

public class BulletSpeedModifier : MonoBehaviour
{
    [SerializeField] private float _bulletSpeedModify = 0;
    private float _baseBulletSpeed = 3f;

    public float BulletSpeed =>  _baseBulletSpeed * (1f + _bulletSpeedModify);
}
