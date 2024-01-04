using System.Collections;
using UnityEngine;

public class PlayerParticleSystem : BaseParticleSystemPlayer
{
    [SerializeField] private ParticleSystem _levelUpEffect;
    [SerializeField] private ParticleSystem _shieldEffect;
    private PlayerLevelSystem _playerLevelSystem;
    private float _dustDuration = 0.1f;
    private float _levelUpEffectDuration = 3f;

    public override void PlayEffect(ParticleEffects effects)
    {
        switch (effects)
        {
            case ParticleEffects.Dust:
                Effect.Play();
                break;
        }

        StartCoroutine(StopDustAfterDelay());
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
        var waitForSeconds = new WaitForSeconds(_levelUpEffectDuration);
        yield return waitForSeconds;
        _levelUpEffect.Stop();
    }

    private IEnumerator StopDustAfterDelay()
    {
        var waitForSeconds = new WaitForSeconds(_dustDuration);
        yield return waitForSeconds;
        Effect.Stop();
    }
}
