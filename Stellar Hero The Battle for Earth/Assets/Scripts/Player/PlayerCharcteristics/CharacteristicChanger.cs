using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacteristicChanger : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _description;

    private CharacteristicChangerConfig _config;
    private UpdateCharacteristicWindow _window;

    private void Start()
    {
        _window = GetComponentInParent<UpdateCharacteristicWindow>();
    }

    public void Init(CharacteristicChangerConfig config)
    {
        _config = config;

        _icon.sprite = _config.Icon;
        _description.text = _config.Description;
    }

    public void OnClick()
    {
        ChangeCharacteristic();

        Time.timeScale = 1.0f;
        _window.gameObject.SetActive(false);
    }

    public void ChangeCharacteristic()
    {
        _config.ChangeCharacteristic();
    }
}
