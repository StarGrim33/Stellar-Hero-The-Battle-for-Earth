using System.Collections;
using Player;
using UnityEngine;

namespace Buffs
{
    public class ImmortalityBuff : BaseBuff
    {
        [SerializeField] private float _duration;
        [SerializeField] private SpriteRenderer _sprite;
        private WaitForSeconds _delay;

        private void OnEnable()
        {
            StartCoroutine(LifeTime());
        }

        private void Start()
        {
            _delay = new WaitForSeconds(_duration);
        }

        public override void Take(PlayerHealth playerHealth)
        {
            playerHealth.ActivateImmortal();
            StartCoroutine(DisableEffect());
        }

        private IEnumerator DisableEffect()
        {
            _sprite.enabled = false;
            yield return _delay;
            Destroy(gameObject);
        }

        private IEnumerator LifeTime()
        {
            while (Timer < _duration)
            {
                Timer += Time.deltaTime;
                yield return null;
            }

            Destroy(gameObject);
        }
    }
}
