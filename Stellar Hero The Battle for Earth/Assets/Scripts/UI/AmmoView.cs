using TMPro;
using UnityEngine;

public class AmmoView : MonoBehaviour
{
    [SerializeField] private TMP_Text _ammoText;
    private Pistol _weapon;

    private void OnDestroy()
    {
        if (_weapon != null)
        {
            _weapon.AmmoChanged -= AmmoChanged;
        }
    }

    public void Init(Pistol weapon)
    {
        _weapon = weapon;
        _weapon.AmmoChanged += AmmoChanged;
        AmmoChanged(_weapon.CurrentAmmo, _weapon.MaxAmmo);
    }

    private void AmmoChanged(int value, int maxAmmo)
    {
        _ammoText.text = $"{value} / {maxAmmo}".ToString();
    }
}
