using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class EnemyPool : MonoBehaviour, IEnemyPool
    {
        public static EnemyPool EnemyPoolInstance { get; private set; }

        [SerializeField] private GameObject _container;
        [SerializeField] private List<EnemyInfo> _enemyInfos;

        private Dictionary<GameObject, List<GameObject>> _enemyDictionary;

        private void Awake()
        {
            if (EnemyPoolInstance == null)
            {
                EnemyPoolInstance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            InitializeEnemyDictionary();
        }

        private void Start()
        {
            foreach (var enemyInfo in _enemyInfos)
            {
                CreateObjects(enemyInfo.Prefab, _container.transform, enemyInfo.Amount, enemyInfo.List);
            }
        }

        public GameObject GetObject(GameObject prefab)
        {
            if (_enemyDictionary.TryGetValue(prefab, out var list))
            {
                foreach (var obj in list)
                {
                    if (!obj.activeInHierarchy)
                    {
                        return obj;
                    }
                }
            }

            return null;
        }

        private void InitializeEnemyDictionary()
        {
            _enemyDictionary = new Dictionary<GameObject, List<GameObject>>();

            foreach (var enemyInfo in _enemyInfos)
            {
                _enemyDictionary.Add(enemyInfo.Prefab, enemyInfo.List);
            }
        }

        private void CreateObjects(GameObject prefab, Transform parent, int count, List<GameObject> list)
        {
            for (int i = 0; i < count; i++)
            {
                var obj = Instantiate(prefab, parent);
                obj.SetActive(false);
                list.Add(obj);
            }
        }
    }
}