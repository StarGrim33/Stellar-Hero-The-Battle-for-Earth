using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool EnemyPoolInstance;

    [SerializeField] private int _amount;
    [SerializeField] private GameObject _simpleEnemyPrefab;
    [SerializeField] private List<GameObject> _simpleEnemyList;
    [SerializeField] private GameObject _container;

    [SerializeField] private int _bomberEnemyAmount;
    [SerializeField] private GameObject _bomberEnemy;
    [SerializeField] private List<GameObject> _bomberEnemyList;

    private void Awake()
    {
        EnemyPoolInstance = this;
    }

    private void Start()
    {
        CreateObjects(_simpleEnemyPrefab, _container.transform, _amount, _simpleEnemyList);
        CreateObjects(_bomberEnemy, _container.transform, _bomberEnemyAmount, _bomberEnemyList);
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
