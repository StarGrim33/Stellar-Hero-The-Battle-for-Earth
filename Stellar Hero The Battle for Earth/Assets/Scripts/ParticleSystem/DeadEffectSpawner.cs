using System.Collections.Generic;
using UnityEngine;

public class DeadEffectSpawner : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> _particles;

    public void SpawnEffect()
    {
        ParticleSystem particleSystem = TryGetDeadEffect();

        if(particleSystem != null)
        {
            particleSystem.Play();
        }
    }

    private ParticleSystem TryGetDeadEffect()
    {
        var randomEffect = Random.Range(0, _particles.Count);
        return _particles[randomEffect];
    }
}
