using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private PlayerUnit _player;
    [SerializeField] private EnemyPool _enemyPool;

    public event UnityAction AllEnemySpawned;
    public event UnityAction WaveChanged;
    public event UnityAction ReachedLastWave;

    public int WaveCount => _waves.Count;

    public int CurrentWave => _currentWaveIndex;

    public int WavesRemaining => _lastWaveNumber - CurrentWave;

    private Wave _currentWave;
    private int _currentWaveIndex = 0;
    private float _timeAfterLastSpawn;
    private int _spawned;
    private int _lastWaveNumber = 1;
    private int _currentSpawnPointIndex = 0;

    private void Start()
    {
        SetWave(_currentWaveIndex);
    }

    private void Update()
    {
        if (_currentWave == null)
            return;

        _timeAfterLastSpawn += Time.deltaTime;

        if (_timeAfterLastSpawn >= _currentWave.Delay)
        {
            if (_currentSpawnPointIndex < _spawnPoints.Length - 1)
            {
                _currentSpawnPointIndex++;
                SpawnEnemy(_currentSpawnPointIndex);
                _spawned++;
                _timeAfterLastSpawn = 0;
            }
            else
            {
                _currentSpawnPointIndex = 0;
                SpawnEnemy(_currentSpawnPointIndex);
                _spawned++;
                _timeAfterLastSpawn = 0;
            }
        }

        if (_currentWave.Amount <= _spawned)
        {
            if (_waves.Count > _currentWaveIndex + 1)
                AllEnemySpawned?.Invoke();

            _currentWave = null;
        }
    }

    private void SetWave(int index)
    {
        _currentWave = _waves[index];
    }

    private void SpawnEnemy(int spawnPoint)
    {
        GameObject enemy = _enemyPool.GetObject(_currentWave.Template);

        enemy.transform.position = _spawnPoints[spawnPoint].position;
        enemy.transform.rotation = _spawnPoints[spawnPoint].rotation;
        enemy.transform.SetParent(_spawnPoints[spawnPoint]);

        enemy.gameObject.SetActive(true);
        //enemy.GetComponent<EnemyHealth>().Dying += OnEnemyDying;
    }

    private void OnEnemyDying(EnemyHealth enemy)
    {
        //enemy.Dying -= OnEnemyDying;
        enemy.gameObject.SetActive(false);
    }

    public void NextWave()
    {
        if (_currentWaveIndex == _lastWaveNumber)
        {
            ReachedLastWave?.Invoke();
            Debug.Log("Last wave");
        }

        SetWave(++_currentWaveIndex);
        WaveChanged?.Invoke();
        _spawned = 0;
    }
}

[System.Serializable]
public class Wave
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _delay;
    [SerializeField] private int _amount;

    public float Delay => _delay;

    public GameObject Template => _enemyPrefab;

    public float Amount => _amount;
}
