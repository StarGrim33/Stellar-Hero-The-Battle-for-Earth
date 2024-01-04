using UnityEngine;

public abstract class BaseParticleSystemPlayer : MonoBehaviour
{
    [SerializeField] protected ParticleSystem Effect;

    public virtual void PlayEffect(ParticleEffects effects)
    {
        Effect.Play();
    }
}
