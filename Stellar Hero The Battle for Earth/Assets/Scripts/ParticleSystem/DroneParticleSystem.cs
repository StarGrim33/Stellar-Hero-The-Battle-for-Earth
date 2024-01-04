using UnityEngine;

public class DroneParticleSystem : BaseParticleSystemPlayer 
{
    [SerializeField] private ParticleSystem _explosionEffect;

    public override void PlayEffect(ParticleEffects effects)
    {
        _explosionEffect.Play();
    }
}
