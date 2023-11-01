using UnityEngine;

public abstract class ParticleSystemPlayer : MonoBehaviour
{
    [SerializeField] protected ParticleSystem _effect;

    public virtual void PlayEffect()
    {
        _effect.Play();
    }
}
