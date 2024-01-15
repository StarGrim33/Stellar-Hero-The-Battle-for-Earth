using System.Collections;
using Player;
using UnityEngine;

namespace Utils
{
    public class PlayerParticleSystem : BaseParticleSystem
    {
        private readonly float _dustDuration = 0.1f;
        private readonly float _levelUpEffectDuration = 3f;

        [SerializeField] private ParticleSystem _levelUpEffect;
        private PlayerLevelSystem _playerLevelSystem;
        private WaitForSeconds _levelUpDelay;
        private WaitForSeconds _dustDelay;

        private void Start()
        {
            _levelUpDelay = new WaitForSeconds(_levelUpEffectDuration);
            _dustDelay = new WaitForSeconds(_dustDuration);
        }

        public override void PlayEffect()
        {
            base.PlayEffect();
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
            yield return _levelUpDelay;
            _levelUpEffect.Stop();
        }

        private IEnumerator StopDustAfterDelay()
        {
            yield return _dustDelay;
            Effect.Stop();
        }
    }
}