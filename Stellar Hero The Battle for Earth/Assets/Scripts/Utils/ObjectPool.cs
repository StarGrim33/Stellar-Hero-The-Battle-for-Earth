using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils
{
    public class ObjectPool : MonoBehaviour
    {
        private readonly List<GameObject> _pool = new ();
        [SerializeField] private int _capacity;
        [SerializeField] private Transform _container;

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
            result = _pool.FirstOrDefault(p => !p.activeSelf);
            return result != null;
        }
    }
}