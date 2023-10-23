using System.Collections;
using UnityEngine;

public class ShieldHandler : MonoBehaviour
{
    [SerializeField] private PlayerHealth _health;
    [SerializeField] private ParticleSystem _sprite;

    private void OnEnable()
    {
        _health.Immortality += Immortality;
    }

    private void OnDisable()
    {
        _health.Immortality -= Immortality;
    }

    private void Immortality()
    {
        _sprite.Play();
    }
}
