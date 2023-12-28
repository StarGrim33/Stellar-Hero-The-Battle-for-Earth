using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacteristics : MonoBehaviour
{
    public static PlayerCharacteristics I = null;

    private float _speed = 3f;
    private float _damage = 25f;
    private float _shotCooldown = 1f;
    private float _maxHealth = 100f;
    private float _maxAmmo = 6f;
    private float _reloadTimeAmmo = 1f;
    private float _dushCooldown = 2f;
    private float _playerTriggerDamage = 0f;

    private float _droneTriggerDamage = 0f;
    private float _droneDamage = 10f;
    private float _droneShotDelay = 2f;

    private Dictionary<Characteristics, float> _characteristics = new();

    public Action CharacteristicChanged;

    private void Awake()
    {
        if (I == null)
            I = this;
        else if (I == this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        Initialize();
    }

    public float GetValue(Characteristics characteristic)
    {
        if (_characteristics.TryGetValue(characteristic, out float value))
        {
            return value;
        }
        else
        {
            Debug.LogWarning("Not found: " + characteristic);
            return 0.0f;
        }
    }

    public void SetValue(Characteristics characteristic, float value)
    {
        if (_characteristics.ContainsKey(characteristic))
        {
            _characteristics[characteristic] = value;
            CharacteristicChanged?.Invoke();
        }
        else
        {
            Debug.LogWarning("Not Found: " + characteristic);
        }
    }

    public void AddValue(Characteristics characteristic, float value)
    {
        if (_characteristics.ContainsKey(characteristic))
        {
            _characteristics[characteristic] += value;
            CharacteristicChanged?.Invoke();
        }
        else
        {
            Debug.LogWarning("Not Found: " + characteristic);
        }
    }

    public void Reset()
    {
        Initialize();
    }

    private void Initialize()
    {
        _characteristics[Characteristics.Speed] = _speed;
        _characteristics[Characteristics.Damage] = _damage;
        _characteristics[Characteristics.ShotCooldown] = _shotCooldown;
        _characteristics[Characteristics.MaxHealth] = _maxHealth;
        _characteristics[Characteristics.MaxAmmo] = _maxAmmo;
        _characteristics[Characteristics.ReloadTimeAmmo] = _reloadTimeAmmo;
        _characteristics[Characteristics.DushCooldown] = _dushCooldown;
        _characteristics[Characteristics.PlayerTriggerDamage] = _playerTriggerDamage;
        _characteristics[Characteristics.DroneTriggerDamage] = _droneTriggerDamage;
        _characteristics[Characteristics.DroneDamage] = _droneDamage;
        _characteristics[Characteristics.DroneShotDelay] = _droneShotDelay;
    }
}

public enum Characteristics
{
    Speed,
    Damage,
    ShotCooldown,
    MaxHealth,
    MaxAmmo,
    ReloadTimeAmmo,
    DushCooldown,
    PlayerTriggerDamage,
    DroneTriggerDamage,
    DroneDamage,
    DroneShotDelay
}
