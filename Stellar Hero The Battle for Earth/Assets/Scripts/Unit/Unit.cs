using UnityEngine;

namespace Utils
{
    public abstract class Unit : MonoBehaviour
    {
        [SerializeField] private UnitConfig _unitConfig;

        public UnitConfig Config => _unitConfig;
    }
}