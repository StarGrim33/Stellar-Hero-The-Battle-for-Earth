using System.Collections;
using UnityEngine;

public abstract class Buff : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer Sprite;
    [SerializeField] protected float Duration;

    public float Timer { get; set; }

    public abstract void Take(PlayerHealth playerHealth);

    private void OnEnable()
    {
        StartCoroutine(LifeTime());
    }

    protected IEnumerator LifeTime()
    {
        while (Timer < Duration)
        {
            Timer += Time.deltaTime;
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
