using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] private float _shakeDuration;
    [SerializeField] private float _shakeStrength;

    private Vector3 _originalPosition;

    private void Start()
    {
        _originalPosition = transform.localPosition;
    }

    public void Shake()
    {
        transform.DOShakePosition(_shakeDuration, _shakeStrength)
            .OnComplete(() => ResetPosition());
    }

    private void ResetPosition()
    {
        transform.localPosition = _originalPosition;
    }
}
