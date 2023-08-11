using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFiller : MonoBehaviour
{
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void SetFilled(float value)
    {
        value = Mathf.Clamp01(value);

        _image.fillAmount = value;
    }

    public void StartFilled(float delay)
    {
        StartCoroutine(Filled(delay));
    }

    private IEnumerator Filled(float delay)
    {
        var timer = 0f;
        while (delay >= timer)
        {
            timer += Time.deltaTime;
            _image.fillAmount = timer / delay;
            yield return null;
        }
    }
}
