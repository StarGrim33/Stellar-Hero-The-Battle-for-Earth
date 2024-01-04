using DG.Tweening;
using TMPro;
using UnityEngine;

public class DamagePopuper : MonoBehaviour
{
    [SerializeField] private GameObject _prefabPopup;
    [SerializeField] private TMP_Text _text;
    private float _maxShiftDistance = 0.5f;

    private void Start()
    {
        _prefabPopup.SetActive(false);
    }

    public void ShowDamagePopup(int damage)
    {
        float duration = 0.2f;
        float scaleValue = 0.1f;
        float scaleDuration = 0.2f;
        _text.text = damage.ToString();

        Vector3 randomShift = new Vector3(
            Random.Range(-_maxShiftDistance, _maxShiftDistance),
            Random.Range(-_maxShiftDistance, _maxShiftDistance),
            0
        );

        _prefabPopup.transform.position = transform.position + randomShift;
        _prefabPopup.SetActive(true);
        _prefabPopup.transform.DOScale(Vector3.one * duration, duration)
                    .OnComplete(() => _prefabPopup.transform.DOScale(Vector3.one * scaleValue, scaleDuration)
                    .OnComplete(() => HidePopup()));
    }

    private void HidePopup()
    {
        _prefabPopup.SetActive(false);
    }
}
