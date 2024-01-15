using System.Collections;
using UnityEngine;

namespace Utils
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class MaterialBlicker : MonoBehaviour
    {
        [SerializeField] private Material _materialFlash;
        [SerializeField] private float _flashDuration;
        private SpriteRenderer _spriteRenderer;
        private Material _originalMaterial;
        private WaitForSeconds _delay;

        private void Start()
        {
            Init();
        }

        public void Flash()
        {
            if (!gameObject.activeSelf)
                return;

            _spriteRenderer.material = _materialFlash;
            StartCoroutine(ResetMaterial());
        }

        private IEnumerator ResetMaterial()
        {
            yield return _delay;
            _spriteRenderer.material = _originalMaterial;
        }

        private void Init()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _originalMaterial = _spriteRenderer.material;
            _delay = new WaitForSeconds(_flashDuration);
        }
    }
}