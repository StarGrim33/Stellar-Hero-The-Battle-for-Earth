using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Pistol))]
public class WeaponAnimator : MonoBehaviour
{
    [SerializeField] private Transform _weaponTransform;
    private Pistol _pistol;
    private Quaternion _startRotation;

    private void Awake()
    {
        _pistol = GetComponent<Pistol>();
        _startRotation = _weaponTransform.localRotation;
    }

    private void OnEnable()
    {
        _pistol.Reloading += Reloading;
    }

    private void OnDisable()
    {
        _pistol.Reloading -= Reloading;
    }

    private void Reloading(bool reloading)
    {
        float zeroAngle = 0f;
        float completeAngle = 360f;
        float duration = 0.2f;
        int loop = -1;

        if (reloading)
        {
            _weaponTransform
            .DOLocalRotate(new Vector3(zeroAngle, zeroAngle, completeAngle), duration, RotateMode.FastBeyond360)
            .SetLoops(loop, LoopType.Restart)
            .SetEase(Ease.Linear).SetLink(_weaponTransform.gameObject);
        }
        else
        {
            _weaponTransform.DOKill();
            _weaponTransform.localRotation = _startRotation;
        }
    }
}
