using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    public static Spawner Instance { get; private set; }

    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _delayBetweenWaves;
    [SerializeField] private EnemyPool _enemyPool;
    [SerializeField] private List<WaveEnemies> _waves;
    [SerializeField] private PlayerUnit _playerUnit;
    [SerializeField] private List<int> _unusedSpawnPoints;

    private IDamageable _target;

    public int CurrentWaveIndex => _currentWaveIndex;

    public event UnityAction AllEnemySpawned;

    private ExperienceHandler _experienceHandler;
    private int _currentWaveIndex = 0;
    private WaveEnemies _currentWave;
    private float _timeAfterLastSpawn;
    private int _spawned;
    private int _lastWaveNumber = 1;
    private int _currentSpawnPointIndex = 1;
    private bool _isSpawnFrozen = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Debug.LogWarning("Multiple instances of Spawner found. Only one instance should exist.");
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        _experienceHandler = GetComponent<ExperienceHandler>();
        _unusedSpawnPoints = new List<int>();

        for (int i = 0; i < _spawnPoints.Length; i++)
            _unusedSpawnPoints.Add(i);

        SetWave(_currentWaveIndex);
        _target = _playerUnit.GetComponent<IDamageable>();
    }

    private void Update()
    {
        if (_isSpawnFrozen || StateManager.Instance.CurrentGameState == GameStates.Paused)
            return;

        if (_currentWave == null)
            return;

        _timeAfterLastSpawn += Time.deltaTime;

        if (_timeAfterLastSpawn >= _currentWave.SpawnDelay)
        {
            if (_currentSpawnPointIndex < _spawnPoints.Length - 1)
            {
                _currentSpawnPointIndex++;
                SpawnEnemy();
                _spawned++;
                _timeAfterLastSpawn = 0;
            }
            else
            {
                _currentSpawnPointIndex = 0;
                SpawnEnemy();
                _spawned++;
                _timeAfterLastSpawn = 0;
            }
        }

        if (_currentWave.Amount <= _spawned)
        {
            if (_waves.Count > _currentWaveIndex + 1)
            {
                AllEnemySpawned?.Invoke();
                StartCoroutine(NextWave());
            }

            _currentWave = null;
        }
    }

    public void FreezeSpawn(float duration)
    {
        _isSpawnFrozen = true;
        StartCoroutine(UnfreezeSpawnAfterDelay(duration));
    }

    public IEnumerator NextWave()
    {
        var delay = new WaitForSeconds(_delayBetweenWaves);

        yield return delay;

        SetWave(++_currentWaveIndex);
        _spawned = 0;
    }

    private IEnumerator UnfreezeSpawnAfterDelay(float duration)
    {
        var delay = new WaitForSeconds(duration);

        yield return delay;
        _isSpawnFrozen = false;
    }

    private void SpawnEnemy()
    {
        int randomIndex = UnityEngine.Random.Range(0, _unusedSpawnPoints.Count);

        if (TryGetSpawnPoint(randomIndex, out int spawnPointIndex))
            _unusedSpawnPoints.RemoveAt(randomIndex);
        else
        {
            ResetSpawnPoints();
            randomIndex = UnityEngine.Random.Range(0, _unusedSpawnPoints.Count);
            TryGetSpawnPoint(randomIndex, out spawnPointIndex);
        }

        GameObject enemy = _enemyPool.GetObject(_currentWave.EnemyPrefab);

        enemy.transform.position = _spawnPoints[spawnPointIndex].position;
        enemy.transform.rotation = _spawnPoints[spawnPointIndex].rotation;
        enemy.gameObject.SetActive(true);
        enemy.GetComponent<EnemyStateMachine>().SetTarget(_target);
        enemy.GetComponent<EnemyHealth>().Dying += OnEnemyDying;
    }

    private void OnEnemyDying(EnemyHealth enemy)
    {
        if(enemy == null) return;

        if(enemy.TryGetComponent<ExperienceEnemy>(out ExperienceEnemy experience))
            _experienceHandler.AddExperience(experience.ExperienceForEnemy);

        enemy.Dying -= OnEnemyDying;
    }

    private void SetWave(int index)
    {
        _currentWave = _waves[index];
    }

    private bool TryGetSpawnPoint(int randomIndex, out int spawnPointIndex)
    {
        spawnPointIndex = -1;

        if (_unusedSpawnPoints.Contains(randomIndex))
        {
            spawnPointIndex = _unusedSpawnPoints[randomIndex];
            return true;
        }

        return false;
    }

    private void ResetSpawnPoints()
    {
        _unusedSpawnPoints.Clear();

        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            _unusedSpawnPoints.Add(i);
        }
    }
}
