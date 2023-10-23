using System.Collections.Generic;
using UnityEngine;

public class EnemyBuffDropper : MonoBehaviour
{
    [SerializeField] private List<Buff> _buffs;
    [SerializeField, Range(0f, 100f)] private float _spawnProbability = 10f;

    public void SpawnRandomBuff()
    {
        if (Random.Range(0f, 100f) <= _spawnProbability)
        {
            Buff buff = TryGetRandomBuff();

            if (buff != null)
            {
                var instance = Instantiate(buff);
                instance.transform.position = this.transform.position;
                buff.Timer = 0;
            }
        }
    }

    private Buff TryGetRandomBuff()
    {
        return _buffs.Count > 0 ? _buffs[Random.Range(0, _buffs.Count)] : null;
    }
}
