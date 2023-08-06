using UnityEngine;

public class PoolObjectSpawnComponent : ObjectPool
{
    [SerializeField] private GameObject _template;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private bool _isTransformToLossyScale = true;

    private void Start()
    {
        Initialize(_template);
    }

    public GameObject Spawn()
    {
        if(TryGetObject(out GameObject tamplate))
        {
            tamplate.transform.position = _spawnPoint.position;
            if(_isTransformToLossyScale )
                tamplate.transform.localScale = _spawnPoint.lossyScale;

            tamplate.SetActive(true);
        }

        return tamplate;
    }
}
