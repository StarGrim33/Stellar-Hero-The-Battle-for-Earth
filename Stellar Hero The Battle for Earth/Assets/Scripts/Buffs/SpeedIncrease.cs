using System.Collections;
using Player;
using UnityEngine;

namespace Buffs
{
    public class SpeedIncrease : BaseBuff
    {
        [SerializeField] private float _speedValue = 4;
        [SerializeField] private float _duration = 5;
        private WaitForSeconds _delay;
        private SpriteRenderer _sprite;
        private float _lifeTime = 6f;
        private float _currentSpeed;

        private void Awake()
        {
            _sprite = GetComponent<SpriteRenderer>();
            _delay = new WaitForSeconds(_duration);
        }

        private void OnEnable()
        {
            StartCoroutine(LifeTime());
        }

        public override void Take(PlayerHealth playerHealth)
        {
            _currentSpeed = (int)PlayerCharacteristics.I.GetValue(Characteristics.Speed);
            PlayerCharacteristics.I.AddValue(Characteristics.Speed, _speedValue);
            _sprite.enabled = false;
            StartCoroutine(ReturnToBase());
        }

        private IEnumerator ReturnToBase()
        {
            yield return _delay;
            PlayerCharacteristics.I.SetValue(Characteristics.Speed, _currentSpeed);
        }

        private IEnumerator LifeTime()
        {
            while (Timer < _lifeTime)
            {
                Timer += Time.deltaTime;
                yield return null;
            }

            Destroy(gameObject);
        }
    }
}
