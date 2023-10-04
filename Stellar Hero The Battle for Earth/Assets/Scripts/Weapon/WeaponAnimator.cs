using DG.Tweening;
using UnityEngine;

public class WeaponAnimator : MonoBehaviour
{
    [SerializeField] private Transform weaponTransform;
    private Pistol _pistol;

    private Quaternion _startRotation; 

    private void Awake()
    {
        _pistol = GetComponent<Pistol>();

        _startRotation = weaponTransform.localRotation;
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
        if (reloading)
        {
            weaponTransform
                .DOLocalRotate(new Vector3(0f, 0f, 360f), 0.3f, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Restart) 
                .SetEase(Ease.Linear); 
        }
        else
        {
            weaponTransform.DOKill();
            weaponTransform.localRotation = _startRotation;
        }
    }
}
