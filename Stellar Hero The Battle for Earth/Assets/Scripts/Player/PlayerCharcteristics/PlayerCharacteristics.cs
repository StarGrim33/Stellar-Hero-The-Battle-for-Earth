using System.Collections.Generic;
using UnityEngine;


public class PlayerCharacteristics : MonoBehaviour
{
    public static PlayerCharacteristics I = null;

    private float _speed = 10f;
    private float _damage = 25f;
    private float _attackSpeed = 1f;
    private float _maxHealth = 100f;

    private Dictionary<Characteristics, float> _characteristics = new Dictionary<Characteristics, float>();

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
            Debug.LogWarning("Íĺâĺđíîĺ čě˙ ďŕđŕěĺňđŕ: " + characteristic);
            return 0.0f;
        }
    }

    public void SetValue(Characteristics characteristic, float value)
    {
        switch (characteristic)
        {
            case Characteristics.Speed:
                _speed = value;
                break;
            case Characteristics.Damage:
                _damage = value;
                break;
            case Characteristics.AttackSpeed:
                _attackSpeed = value;
                break;
            case Characteristics.MaxHealth:
                _maxHealth = value;
                break;
            default:
                Debug.LogWarning("Íĺâĺđíîĺ čě˙ ďŕđŕěĺňđŕ: " + characteristic);
                break;
        }
    }

    public void AddValue(Characteristics characteristic, float value)
    {
        switch (characteristic)
        {
            case Characteristics.Speed:
                _speed += value;
                break;
            case Characteristics.Damage:
                _damage += value;
                break;
            case Characteristics.AttackSpeed:
                _attackSpeed += value;
                break;
            case Characteristics.MaxHealth:
                _maxHealth += value;
                break;
            default:
                Debug.LogWarning("Íĺâĺđíîĺ čě˙ ďŕđŕěĺňđŕ: " + characteristic);
                break;
        }
    }

    private void Initialize()
    {
        _characteristics[Characteristics.Speed] = _speed;
        _characteristics[Characteristics.Damage] = _damage;
        _characteristics[Characteristics.AttackSpeed] = _attackSpeed;
        _characteristics[Characteristics.MaxHealth] = _maxHealth;
        _speed = 10f;
        _damage = 1;
        _attackSpeed = 1f;
        _maxHealth = 100f;
    }
}

public enum Characteristics
{
    Speed,
    Damage,
    AttackSpeed,
    MaxHealth
}
