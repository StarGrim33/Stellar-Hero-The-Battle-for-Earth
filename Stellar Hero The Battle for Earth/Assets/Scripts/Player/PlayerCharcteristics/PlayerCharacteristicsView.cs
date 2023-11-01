using TMPro;
using UnityEngine;

public class PlayerCharacteristicsView : MonoBehaviour
{
    [SerializeField] private TMP_Text _characteristicsText;

    private PlayerCharacteristics _characteristics;

    private void Start()
    {
        _characteristics = PlayerCharacteristics.I;
    }

    private void Update()
    {
        _characteristicsText.text = $"Speed = {_characteristics.GetValue(Characteristics.Speed)} \n" +
            $"Damage ={_characteristics.GetValue(Characteristics.Damage)} \n" +
            $"ShotCooldown= {_characteristics.GetValue(Characteristics.ShotCooldown)} \n" +
            $"MaxHealth = {_characteristics.GetValue(Characteristics.MaxHealth)} \n";
    }
}
