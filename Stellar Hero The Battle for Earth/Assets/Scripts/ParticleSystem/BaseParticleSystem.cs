using UnityEngine;

namespace Utils
{
    public abstract class BaseParticleSystem : MonoBehaviour
    {
        [SerializeField] protected ParticleSystem Effect;

        public virtual void PlayEffect() => Effect.Play();
    }
}