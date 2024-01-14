using UnityEngine;

[CreateAssetMenu(menuName = "Source/Units/Config", fileName = "UnitConfig", order = 0)]
public class UnitConfig : ScriptableObject
{
    [SerializeField, Min(0)] private float _health;
    [SerializeField, Min(0)] private float _speed;

    public float Health => _health;

    public float Speed => _speed;
}