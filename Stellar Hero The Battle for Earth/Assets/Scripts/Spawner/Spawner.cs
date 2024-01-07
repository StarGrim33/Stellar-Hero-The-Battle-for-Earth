using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour, ISpawner
{
    public static Spawner Instance { get; private set; }

    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private EnemyPool _enemyPool;
    [SerializeField] private List<WaveEnemies> _waves;
    [SerializeField] private PlayerUnit _playerUnit;
    [SerializeField] private List<int> _unusedSpawnPoints;
    [SerializeField] private BestWaveView _bestWaveView;
    [SerializeField] private float _delayBetweenWaves;

    private LeaderboradSaver _saver;
    private IDamageable _target;
    private ExperienceHandler _experienceHandler;
    private WaveEnemies _currentWave;
    private int _currentWaveIndex = 0;
    private int _currentSpawnPointIndex = 1;
    private bool _isSpawnFrozen = false;
    private float _timeAfterLastSpawn;
    private int _spawned;

    public event Action AllEnemySpawned;
    public event Action<int> WaveChanged;

    public int CurrentWaveIndex => _currentWaveIndex + 1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple instances of Spawner found. Only one instance should exist.");
            Destroy(gameObject);
            return;
        }

        _saver = GetComponent<LeaderboradSaver>();
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (_isSpawnFrozen || StateManager.Instance.CurrentGameState == GameStates.Paused)
        {
            return;
        }

        if (_currentWave == null)
        {
            return;
        }

        SpawnWave();

        if (_currentWave.Amount <= _spawned)
        {
            if (_waves.Count > _currentWaveIndex + 1)
            {
                AllEnemySpawned?.Invoke();
                StartCoroutine(NextWave());
            }

            _currentWave = null;
        }

        if (_currentWaveIndex >= _waves.Count - 1)
        {
            _currentWaveIndex = 0;
            SetWave(_currentWaveIndex);
            _spawned = 0;
            SpawnWave();
            WaveChanged?.Invoke(CurrentWaveIndex);
        }
    }

    public IEnumerator NextWave()
    {
        var delay = new WaitForSeconds(_delayBetweenWaves);

        yield return delay;

        SetWave(++_currentWaveIndex);
        _spawned = 0;

        if (_currentWaveIndex > _bestWaveView.BestWave)
        {
            Saves.Save(Constants.BestWaveKey, _currentWaveIndex++);
            _saver.GetLeaderboardPlayerEntryButtonClick(_currentWaveIndex++);
        }
    }

    public void SpawnWave()
    {
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
    }

    public void SpawnEnemy()
    {
        int randomIndex = UnityEngine.Random.Range(0, _unusedSpawnPoints.Count);

        if (TryGetSpawnPoint(randomIndex, out int spawnPointIndex))
        {
            _unusedSpawnPoints.RemoveAt(randomIndex);
        }
        else
        {
            ResetSpawnPoints();
            randomIndex = UnityEngine.Random.Range(0, _unusedSpawnPoints.Count);
            TryGetSpawnPoint(randomIndex, out spawnPointIndex);
        }

        GameObject enemy = _enemyPool.GetObject(_currentWave.EnemyPrefab);

        enemy.transform.position = _spawnPoints[spawnPointIndex].position;

        if (enemy.transform.position == null)
        {
            throw new Exception("Check spawner and enemy pool for available space");
        }

        enemy.transform.rotation = _spawnPoints[spawnPointIndex].rotation;
        enemy.gameObject.SetActive(true);
        enemy.GetComponent<EnemyStateMachine>().SetTarget(_target);
        enemy.GetComponent<EnemyHealth>().Dying += OnEnemyDying;
    }

    private void OnEnemyDying(EnemyHealth enemy)
    {
        if (enemy == null)
        {
            return;
        }

        if (enemy.TryGetComponent<ExperienceEnemy>(out ExperienceEnemy experience))
        {
            _experienceHandler.AddExperience(experience.ExperienceForEnemy);
        }

        enemy.Dying -= OnEnemyDying;
    }

    public void SetWave(int index)
    {
        _currentWave = _waves[index];

        if (_currentWaveIndex > _bestWaveView.BestWave)
        {
            Saves.Save(Constants.BestWaveKey, _currentWaveIndex++);
            _saver.GetLeaderboardPlayerEntryButtonClick(_currentWaveIndex++);
        }

        WaveChanged?.Invoke(CurrentWaveIndex);
    }

    public bool TryGetSpawnPoint(int randomIndex, out int spawnPointIndex)
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
            _unusedSpawnPoints.Add(i);
    }

    private void Init()
    {
        _experienceHandler = GetComponent<ExperienceHandler>();
        _unusedSpawnPoints = new List<int>();

        for (int i = 0; i < _spawnPoints.Length; i++)
            _unusedSpawnPoints.Add(i);

        SetWave(_currentWaveIndex);
        _target = _playerUnit.GetComponent<IDamageable>();
        WaveChanged?.Invoke(CurrentWaveIndex);
    }
}
