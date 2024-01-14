using UnityEngine;

namespace Core
{
    public abstract class Unit : MonoBehaviour
    {
        [SerializeField] private UnitConfig _unitConfig;

        public UnitConfig Config => _unitConfig;
    }
}