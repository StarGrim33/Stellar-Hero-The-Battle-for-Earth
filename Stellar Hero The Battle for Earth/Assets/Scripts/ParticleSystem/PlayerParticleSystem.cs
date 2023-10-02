using System.Collections;
using UnityEngine;

public class PlayerParticleSystem : ParticleSystemPlayer
{
    [SerializeField] private ParticleSystem _levelUpEffect;
    private float _dustDuration = 0.1f;
    private float _levelUpEffectDuration = 3f;
    private PlayerLevelSystem _playerLevelSystem;

    public override void PlayEffect()
    {
        base.PlayEffect();
        StartCoroutine(StopDustAfterDelay());
    }

    private IEnumerator StopDustAfterDelay()
    {
        yield return new WaitForSeconds(_dustDuration);
        _effect.Stop();
    }

    public void SetLevelSystem(PlayerLevelSystem playerLevelSystem)
    {
        _playerLevelSystem = playerLevelSystem;
        _playerLevelSystem.OnLevelChanged += OnLevelChanged;
    }

    private void OnLevelChanged()
    {
        _levelUpEffect.Play();
        StartCoroutine(StopLevelUpAfterDelay());
    }

    private IEnumerator StopLevelUpAfterDelay()
    {
        yield return new WaitForSeconds(_levelUpEffectDuration);
        _levelUpEffect.Stop();
    }
}
