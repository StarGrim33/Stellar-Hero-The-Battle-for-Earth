using System.Collections;
using UnityEngine;

public class PlayerParticleSystem : ParticleSystemPlayer
{
    [SerializeField] private ParticleSystem _levelUpEffect;
    [SerializeField] private ParticleSystem _shieldEffect;
    private float _dustDuration = 0.1f;
    private float _levelUpEffectDuration = 3f;
    private PlayerLevelSystem _playerLevelSystem;
    private float _shieldDuration = 5;

    public override void PlayEffect(ParticleEffects effects)
    {
        switch (effects)
        {
            case ParticleEffects.Dust:
                _effect.Play();
                break;
        }

        StartCoroutine(StopDustAfterDelay());
    }

    private IEnumerator StopDustAfterDelay()
    {
        yield return new WaitForSeconds(_dustDuration);
        _effect.Stop();
    }
    
    private IEnumerator EnableShield()
    {
        var waitForSeconds = new WaitForSeconds(_shieldDuration);
        yield return waitForSeconds;
        _shieldEffect.Play();
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

public enum ParticleEffects
{
    LevelUp,
    Shield,
    Dust,
}
