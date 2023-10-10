using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuffTemaplate : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private CharacterUpgrade _upgrade;

    private BuffWidget _widget;

    private void Start()
    {
        _icon.sprite = _upgrade.Icon;
        _description.text = _upgrade.Description;

        _widget = GetComponentInParent<BuffWidget>();
    }

    public void SetUpgrade(CharacterUpgrade upgrade) => _upgrade = upgrade; 

    public void Upgrade()
    {
        Debug.Log("Upgrade");
        _widget.SetSpawner(_upgrade.NumberOfBulletTypeSpawner);

        _widget.UpgradeParams(_upgrade.DamageModify, _upgrade.AttackSpeedModify, _upgrade.BulletSpeedModify);
        _widget.UpgradeSpawner(_upgrade.Count, _upgrade.TypePower);

        if(_upgrade.IsChangeType)
            _widget.SetBulletType(_upgrade.Type);

        _widget.ChangeUpgradeList(_upgrade);

        _widget.HideWidget();
    }
}
