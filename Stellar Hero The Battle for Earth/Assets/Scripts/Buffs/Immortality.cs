using System.Collections;
using UnityEngine;

public class Immortality : BaseBuff
{
    [SerializeField] private float _duration;
    [SerializeField] private SpriteRenderer _sprite;

    private void OnEnable()
    {
        StartCoroutine(LifeTime());
    }

    public override void Take(PlayerHealth playerHealth)
    {
        playerHealth.ActivateImmortal();
        StartCoroutine(DisableEffect());
    }

    private IEnumerator DisableEffect()
    {
        _sprite.enabled = false;
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
