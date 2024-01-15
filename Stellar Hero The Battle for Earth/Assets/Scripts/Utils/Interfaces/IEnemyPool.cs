using UnityEngine;

namespace Utils
{
    public interface IEnemyPool
    {
        GameObject GetObject(GameObject prefab);
    }
}