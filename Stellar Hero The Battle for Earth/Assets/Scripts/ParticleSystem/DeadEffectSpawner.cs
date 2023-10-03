using System.Collections.Generic;
using UnityEngine;

public class DeadEffectSpawner : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> _particles;

    public void SpawnEffect()
    {
        ParticleSystem particleSystem = TryGetDeadEffect();
        particleSystem?.Play();
    }

    private ParticleSystem TryGetDeadEffect()
    {
        return _particles.Count > 0 ? _particles[Random.Range(0, _particles.Count)] : null;
    }
}
