using UnityEngine;

public class ParticleSystemPlayer : MonoBehaviour
{
    [SerializeField] private ParticleSystem _effect;

    public void CreateDust()
    {
        _effect.Play();
    }
}
