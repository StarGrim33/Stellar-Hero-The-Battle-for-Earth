using DG.Tweening;
using TMPro;
using UnityEngine;

public class DamagePopuper : MonoBehaviour
{
    [SerializeField] private GameObject _prefabPopup;
    [SerializeField] private TMP_Text _text;
    private float _displayDuration = 1.0f;
    private float _maxShiftDistance = 0.5f; 

    private void Start()
    {
        _prefabPopup.SetActive(false);
    }

    public void ShowDamagePopup(int damage)
    {
        _text.text = damage.ToString();

        Vector3 randomShift = new Vector3(
            Random.Range(-_maxShiftDistance, _maxShiftDistance),
            Random.Range(-_maxShiftDistance, _maxShiftDistance),
            0
        );

        _prefabPopup.transform.position = transform.position + randomShift;
        _prefabPopup.SetActive(true);
        _prefabPopup.transform.DOScale(Vector3.one * 0.2f, 0.2f)
                    .OnComplete(() => _prefabPopup.transform.DOScale(Vector3.one * 0.1f, 0.2f)
                    .OnComplete(() => HidePopup()));
    }

    private void HidePopup()
    {
        _prefabPopup.SetActive(false);
    }
}
