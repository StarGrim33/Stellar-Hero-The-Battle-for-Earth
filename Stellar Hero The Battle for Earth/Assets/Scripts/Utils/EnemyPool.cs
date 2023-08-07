using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool EnemyPoolInstance;

    [SerializeField] private int _amount;
    [SerializeField] private GameObject _simpleEnemyPrefab;
    [SerializeField] private List<GameObject> _simpleEnemyList;
    [SerializeField] private GameObject _container;

    private void Awake()
    {
        EnemyPoolInstance = this;
        CreateObjects(_simpleEnemyPrefab, _container.transform, _amount, _simpleEnemyList);
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
        else
            return null;

        for (int i = 0; i < list.Count; i++)
        {
            if (!list[i].activeInHierarchy)
                return list[i];
        }

        return null;
    }

    public void ReturnToPool()
    {
        gameObject.SetActive(false);
    }
}
