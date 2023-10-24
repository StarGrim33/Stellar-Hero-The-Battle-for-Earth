using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool EnemyPoolInstance;
    [SerializeField] private GameObject _container;

    [SerializeField] private int _amount;
    [SerializeField] private GameObject _simpleEnemyPrefab;
    [SerializeField] private List<GameObject> _simpleEnemyList;

    [SerializeField] private int _bomberEnemyAmount;
    [SerializeField] private GameObject _bomberEnemy;
    [SerializeField] private List<GameObject> _bomberEnemyList;

    [SerializeField] private int _tankEnemyAmount;
    [SerializeField] private GameObject _tankPrefab;
    [SerializeField] private List<GameObject> _tankList;

    [SerializeField] private int _knightAmount;
    [SerializeField] private GameObject _knightPrefab;
    [SerializeField] private List<GameObject> _knightList;

    [SerializeField] private int _bossAmount;
    [SerializeField] private GameObject _bossPrefab;
    [SerializeField] private List<GameObject> _bossList;

    private void Awake()
    {
        EnemyPoolInstance = this;
    }

    private void Start()
    {
        CreateObjects(prefab: _simpleEnemyPrefab, parent: _container.transform, count: _amount, list: _simpleEnemyList);
        CreateObjects(prefab: _bomberEnemy, parent: _container.transform, count: _bomberEnemyAmount, list: _bomberEnemyList);
        CreateObjects(prefab: _tankPrefab, parent: _container.transform, count: _tankEnemyAmount, list: _tankList);
        CreateObjects(prefab: _knightPrefab, parent: _container.transform, count: _knightAmount, list: _knightList);
        CreateObjects(prefab: _bossPrefab, parent: _container.transform, count: _bossAmount, list: _bossList);
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
        List<GameObject> list = null;

        if (prefab == _simpleEnemyPrefab)
            list = _simpleEnemyList;
        else if(prefab == _bomberEnemy)
            list = _bomberEnemyList;
        else if(prefab == _tankPrefab)
            list = _tankList;
        else if(prefab == _knightPrefab) 
            list = _knightList;
        else if(prefab == _bossPrefab)
            list = _bossList;
        else
            return null;

        for (int i = 0; i < list.Count; i++)
        {
            if (!list[i].activeInHierarchy)
                return list[i];
        }

        return null;
    }
}
