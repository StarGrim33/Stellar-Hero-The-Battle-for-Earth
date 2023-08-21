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
        Invoke("HidePopup", _displayDuration);
    }

    private void HidePopup()
    {
        _prefabPopup.SetActive(false);
    }
}
