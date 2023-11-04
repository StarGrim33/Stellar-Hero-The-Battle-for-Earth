using System.Collections;
using UnityEngine;

public class SpeedIncrease : Buff
{
    [SerializeField] private float _speedValue = 4;
    [SerializeField] private float _duration = 5;
    private float _lifeTime = 6f;
    private SpriteRenderer _sprite;
    private float _startSpeed;
    private float _currentSpeed;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        Debug.Log($"Currect speed {(int)PlayerCharacteristics.I.GetValue(Characteristics.Speed)}");

        StartCoroutine(LifeTime());
    }

    public override void Take(PlayerHealth playerHealth)
    {
        _currentSpeed = (int)PlayerCharacteristics.I.GetValue(Characteristics.Speed);

        PlayerCharacteristics.I.AddValue(Characteristics.Speed, _speedValue);
        _sprite.enabled = false;
        Debug.Log($"Current speed {(int)PlayerCharacteristics.I.GetValue(Characteristics.Speed)}");
        StartCoroutine(ReturnToBase());
    }

    private IEnumerator ReturnToBase()
    {
        var waitForSeconds = new WaitForSeconds(_duration);
        yield return waitForSeconds;
        PlayerCharacteristics.I.SetValue(Characteristics.Speed, _currentSpeed); // Восстановление скорости
        Debug.Log($"Current speed {(int)PlayerCharacteristics.I.GetValue(Characteristics.Speed)}");
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
