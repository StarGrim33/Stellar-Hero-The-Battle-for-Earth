using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCharacteristics : MonoBehaviour
{
    public static PlayerCharacteristics I = null;

    private float _speed = 10f;
    private float _damage = 25f;
    private float _attackSpeed = 1f;
    private float _maxHealth = 100f;

    private Dictionary<Characteristics, float> characteristics = new Dictionary<Characteristics, float>();

    public UnityAction CharacteristicChanged;

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
        if (characteristics.ContainsKey(characteristic))
        {
            characteristics[characteristic] = value;
            CharacteristicChanged?.Invoke();
        }
        else
        {
            Debug.LogWarning("Not Found: " + characteristic);
        }
    }

    public void AddValue(Characteristics characteristic, float value)
    {
        if (characteristics.ContainsKey(characteristic))
        {
            CharacteristicChanged?.Invoke();
            characteristics[characteristic] += value;
        }
        else
        {
            Debug.LogWarning("Not Found: " + characteristic);
        }
    }

    private void Initialize()
    {
        //Load start characteristics
        characteristics[Characteristics.Speed] = _speed;
        characteristics[Characteristics.Damage] = _damage;
        characteristics[Characteristics.AttackSpeed] = _attackSpeed;
        characteristics[Characteristics.MaxHealth] = _maxHealth;
    }
}

public enum Characteristics
{
    Speed,
    Damage,
    AttackSpeed,
    MaxHealth
}
