using UnityEngine;

public abstract class BaseParticleSystemPlayer : MonoBehaviour
{
    [SerializeField] protected ParticleSystem _effect;

    public virtual void PlayEffect(ParticleEffects effects)
    {
        _effect.Play();
    }
}
