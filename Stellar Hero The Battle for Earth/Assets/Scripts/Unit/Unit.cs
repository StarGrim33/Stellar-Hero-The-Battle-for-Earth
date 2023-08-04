using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] private UnitConfig unitConfig;

    public UnitConfig Config => unitConfig;
}