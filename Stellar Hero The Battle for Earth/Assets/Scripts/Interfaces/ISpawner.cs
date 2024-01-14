using System.Collections;

namespace Enemy
{
    public interface ISpawner
    {
        void SpawnWave();

        void SpawnEnemy();

        void SetWave(int index);

        bool TryGetSpawnPoint(int randomIndex, out int spawnPointIndex);

        IEnumerator NextWave();
    }
}