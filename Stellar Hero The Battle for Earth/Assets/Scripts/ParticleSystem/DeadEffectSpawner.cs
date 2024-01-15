using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class DeadEffectSpawner : MonoBehaviour
    {
        [SerializeField] private List<ParticleSystem> _particles;

        public void SpawnRandomEffect()
        {
            ParticleSystem particleSystem = TryGetDeadEffect();
            particleSystem?.Play();
        }

        public void SpawnEffect(ParticleSystem particleSystem)
        {
            if (particleSystem == null)
                return;

            particleSystem.Play();
        }

        private ParticleSystem TryGetDeadEffect()
        {
            return _particles.Count > 0 ? _particles[Random.Range(0, _particles.Count)] : null;
        }
    }
}