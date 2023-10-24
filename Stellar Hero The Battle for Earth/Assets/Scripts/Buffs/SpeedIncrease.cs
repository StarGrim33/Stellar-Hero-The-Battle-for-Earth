using System.Collections;
using UnityEngine;

public class SpeedIncrease : Buff
{
    [SerializeField]  private float _speedValue = 4;
    [SerializeField] private float _duration = 5;
    private SpriteRenderer _sprite;
    private float _startSpeed;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        StartCoroutine(LifeTime());
    }

    public override void Take(PlayerHealth playerHealth)
    {
        _startSpeed = (int)PlayerCharacteristics.I.GetValue(Characteristics.Speed);
        PlayerCharacteristics.I.AddValue(Characteristics.Speed, _speedValue);
        _sprite.enabled = false;
        StartCoroutine(ReturnToBase());
    }

    private IEnumerator ReturnToBase()
    {
        var waitForSeconds = new WaitForSeconds(_duration);
        yield return waitForSeconds;
        PlayerCharacteristics.I.SetValue(Characteristics.Speed, _startSpeed);
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
