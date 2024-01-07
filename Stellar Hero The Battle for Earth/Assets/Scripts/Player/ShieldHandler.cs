using UnityEngine;

public class ShieldHandler : MonoBehaviour
{
    [SerializeField] private PlayerHealth _health;
    [SerializeField] private ParticleSystem _particle;

    private void OnEnable() => _health.Immortality += Immortality;

    private void OnDisable() => _health.Immortality -= Immortality;

    private void Immortality()
    {
        if(_particle.isPlaying)
        {
            _particle.Stop();
            _particle.Play();
        }

        _particle.Play();
    }
}
