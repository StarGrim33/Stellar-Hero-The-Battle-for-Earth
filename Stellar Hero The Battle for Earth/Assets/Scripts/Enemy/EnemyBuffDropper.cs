using Buffs;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyBuffDropper : MonoBehaviour
    {
        private const float MinRange = 0f;
        private const float MaxRange = 100f;
        private const float SpawnProbability = 10f;

        [SerializeField] private List<BaseBuff> _buffs;
        [SerializeField, Range(MinRange, MaxRange)] private float _spawnProbability = SpawnProbability;

        public void SpawnRandomBuff()
        {
            if (Random.Range(MinRange, MaxRange) <= _spawnProbability)
            {
                BaseBuff buff = TryGetRandomBuff();

                if (buff != null)
                {
                    var instance = Instantiate(buff);
                    instance.transform.position = this.transform.position;
                    buff.Timer = 0;
                }
            }
        }

        private BaseBuff TryGetRandomBuff()
        {
            return _buffs.Count > 0 ? _buffs[Random.Range(0, _buffs.Count)] : null;
        }
    }
}
