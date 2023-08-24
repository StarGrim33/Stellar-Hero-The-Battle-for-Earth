using System.Collections;
using UnityEngine;

public class ParticleSystemPlayer : MonoBehaviour
{
    [SerializeField] private ParticleSystem _effect;
    [SerializeField] private float _flashDuration = 0.1f; 

    public void PlayEffect()
    {
        _effect.Play();
        StartCoroutine(StopEffectAfterDelay());
    }

    private IEnumerator StopEffectAfterDelay()
    {
        yield return new WaitForSeconds(_flashDuration);
        _effect.Stop();
    }
}
