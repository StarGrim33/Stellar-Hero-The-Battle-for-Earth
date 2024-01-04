using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private int _capacity;
    [SerializeField] private Transform _container;
    private readonly List<GameObject> _pool = new();

    public void ResetPool()
    {
        foreach (var item in _pool)
        {
            item.SetActive(false);
        }
    }

    public int FreeObjectsInPool()
    {
        int count = 0;

        foreach (var item in _pool)
        {
            if(item.activeSelf == false)
                count++;
        }

        return count;
    }

    protected void Initialize(GameObject prefab)
    { 
        for (int i = 0; i < _capacity; i++)
        {
            GameObject spawned = Instantiate(prefab, _container);
            spawned.SetActive(false);

            _pool.Add(spawned);
        }
    }

    protected bool TryGetObject(out GameObject result)
    {
        result = _pool.FirstOrDefault(p => p.activeSelf == false);
        return result != null;
    }
}