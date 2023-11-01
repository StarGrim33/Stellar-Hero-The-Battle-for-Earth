using UnityEngine;

[RequireComponent (typeof(EnterTriggerDamage))]
public class DroneTriggerDamageSetter : MonoBehaviour
{
    private EnterTriggerDamage _triggerDamage;
    private PlayerCharacteristics _playerCharacteristics;

    private void Start()
    {
        _triggerDamage = GetComponent<EnterTriggerDamage>();
        _playerCharacteristics = PlayerCharacteristics.I;

        SetDamage();

        _playerCharacteristics.CharacteristicChanged += SetDamage;
    }

    private void SetDamage()
    {
        _triggerDamage.SetDamage((int)_playerCharacteristics.GetValue(Characteristics.DroneTriggerDamage));
    }
}
