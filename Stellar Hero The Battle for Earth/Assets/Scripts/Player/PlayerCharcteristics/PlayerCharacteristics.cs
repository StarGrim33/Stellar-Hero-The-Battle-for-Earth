using System.Collections.Generic;
using UnityEngine;


public class PlayerCharacteristics : MonoBehaviour
{
    public static PlayerCharacteristics I = null;

    [SerializeField] private float _speed = 1.0f;
    [SerializeField] private float _damage = 1.0f;
    [SerializeField] private float _attackSpeed = 1.0f;
    [SerializeField] private float _maxHealth = 100f;

    private Dictionary<Characteristics, float> characteristics = new Dictionary<Characteristics, float>();

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
        if (characteristics.TryGetValue(characteristic, out float value))
        {
            return value;
        }
        else
        {
            Debug.LogWarning("Íĺâĺđíîĺ čě˙ ďŕđŕěĺňđŕ: " + characteristic);
            return 0.0f;
        }
    }

    //public void SetValue(Characteristics characteristic, float value)
    //{
    //    switch (characteristic)
    //    {
    //        case Characteristics.Speed:
    //            _speed = value;
    //            break;
    //        case Characteristics.Damage:
    //            _damage = value;
    //            break;
    //        case Characteristics.AttackSpeed:
    //            _attackSpeed = value;
    //            break;
    //        case Characteristics.MaxHealth:
    //            _maxHealth = value;
    //            break;
    //        default:
    //            Debug.LogWarning("Íĺâĺđíîĺ čě˙ ďŕđŕěĺňđŕ: " + characteristic);
    //            break;
    //    }
    //}

    //public void AddValue(Characteristics characteristic, float value)
    //{
    //    if (characteristics.ContainsKey(characteristic))
    //    {
    //        characteristics[characteristic] += value;
    //    }
    //    else
    //    {
    //        Debug.LogWarning("Íĺâĺđíîĺ čě˙ ďŕđŕěĺňđŕ: " + characteristic);
    //        switch (characteristic)
    //        {
    //            case Characteristics.Speed: return _speed;
    //            case Characteristics.Damage: return _damage;
    //            case Characteristics.AttackSpeed: return _attackSpeed;
    //            case Characteristics.MaxHealth: return _maxHealth;
    //            default:
    //                Debug.LogWarning("Íĺâĺđíîĺ čě˙ ďŕđŕěĺňđŕ: " + characteristic);
    //                return 0.0f;
    //        }
    //    }
    //}

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
        //Çŕăđóçęŕ ďŕđŕěĺňđîâ
        characteristics[Characteristics.Speed] = _speed;
        characteristics[Characteristics.Damage] = _damage;
        characteristics[Characteristics.AttackSpeed] = _attackSpeed;
        characteristics[Characteristics.MaxHealth] = _maxHealth;
        _speed = 1f;
        _damage = 1f;
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
