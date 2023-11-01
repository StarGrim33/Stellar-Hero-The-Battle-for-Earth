using System.Collections;
using UnityEngine;

public class MaterialBlicker : MonoBehaviour
{
    [SerializeField] private Material _materialFlash;
    [SerializeField] private float _flashDuration;

    private SpriteRenderer _spriteRenderer;
    private Material _originalMaterial;
    private Coroutine _flashCoroutine;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalMaterial = _spriteRenderer.material;
    }

    public void Flash()
    {
        if (!gameObject.activeSelf) 
            return;

        _spriteRenderer.material = _materialFlash;
        Invoke(nameof(ResetMaterial), _flashDuration);
    }

    private void ResetMaterial()
    {
        _spriteRenderer.material = _originalMaterial;
    }
}
