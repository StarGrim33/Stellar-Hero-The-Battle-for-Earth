using System.Collections;
using UnityEngine;

public class Immortality : Buff
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private float _duration;

    private void OnEnable()
    {
        StartCoroutine(LifeTime());
    }

    public override void Take(PlayerHealth playerHealth)
    {
        playerHealth.ActivateImmortal();
        _sprite.enabled = false;
        StartCoroutine(DisableEffect());
    }

    private IEnumerator DisableEffect()
    {
        var waitForSeconds = new WaitForSeconds(_duration);
        yield return waitForSeconds;
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
