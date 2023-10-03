using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private PlayerHealth _health;
    [SerializeField] private Image _fronHealthBar;
    [SerializeField] private Image _backHealthBar;

    private float _lerpTimer = 0f;
    private float _chipSpeed = 2f;

    private void Update()
    {
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        float fillFront = _fronHealthBar.fillAmount;
        float fillBack = _backHealthBar.fillAmount;
        float hFraction = _health.CurrentHealth / _health.MaxHealth;

        if(fillBack > hFraction)
        {
            _fronHealthBar.fillAmount = hFraction;
            _backHealthBar.color = Color.red;
            _lerpTimer += Time.deltaTime;
            float percentComplete = _lerpTimer / _chipSpeed;
            _backHealthBar.fillAmount = Mathf.Lerp(fillBack, hFraction, percentComplete);
            _lerpTimer = 0f;
        }
        else if(fillFront < hFraction)
        {
            _backHealthBar.color = Color.green;
            _backHealthBar.fillAmount = hFraction;
            _lerpTimer += Time.deltaTime;
            float percentComplete = _lerpTimer / _chipSpeed;
            _fronHealthBar.fillAmount = Mathf.Lerp(fillFront, _backHealthBar.fillAmount, percentComplete);
            _lerpTimer = 0f;
        }
    }
}
