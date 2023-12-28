using UnityEngine;

[CreateAssetMenu(menuName = "Source/Units/Config", fileName = "UnitConfig", order = 0)]
public class UnitConfig : ScriptableObject
{
    [Header("[Name]"), Space]
    [SerializeField] private string _unitName;

    [Header("[Common]"), Space]
    [SerializeField, Min(0)] private float _health;
    [SerializeField, Min(0)] private float _speed;
    [SerializeField, Min(0)] private float _armor;

    public float Health => _health;

    public float Speed => _speed;
}