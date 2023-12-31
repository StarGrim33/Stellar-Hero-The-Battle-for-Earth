using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool EnemyPoolInstance;

    [SerializeField] private GameObject _container;
    [SerializeField] private List<EnemyInfo> _enemyInfos;

    private Dictionary<GameObject, List<GameObject>> _enemyDictionary;

    [System.Serializable]
    public class EnemyInfo
    {
        public GameObject Prefab;
        public int Amount;
        public List<GameObject> List;
    }

    private void Awake()
    {
        EnemyPoolInstance = this;
        InitializeEnemyDictionary();
    }

    private void Start()
    {
        foreach (var enemyInfo in _enemyInfos)
        {
            CreateObjects(enemyInfo.Prefab, _container.transform, enemyInfo.Amount, enemyInfo.List);
        }
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
}
