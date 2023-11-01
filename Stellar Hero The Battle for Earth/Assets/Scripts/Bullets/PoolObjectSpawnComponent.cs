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
        if(TryGetObject(out GameObject template))
        {
            template.transform.position = _spawnPoint.position;

            if(_isTransformToLossyScale)
                template.transform.localScale = _spawnPoint.lossyScale;

            template.SetActive(false);
        }

        return template;
    }
}
