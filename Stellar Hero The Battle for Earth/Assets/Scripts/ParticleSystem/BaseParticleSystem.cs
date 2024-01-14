using UnityEngine;

namespace Core
{
    public abstract class BaseParticleSystem : MonoBehaviour
    {
        [SerializeField] protected ParticleSystem Effect;

        public virtual void PlayEffect() => Effect.Play();
    }
}