using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using Player;
using SDK;
using UnityEngine;

namespace Utils
{
    [RequireComponent(typeof(LeaderboardSaver), typeof(ExperienceHandler))]
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

        private LeaderboardSaver _saver;
        private IDamageable _target;
        private ExperienceHandler _experienceHandler;
        private WaveEnemies _currentWave;
        private WaitForSeconds _wavesDelay;
        private int _currentWaveIndex;
        private int _currentSpawnPointIndex = 1;
        private bool _isSpawnFrozen;
        private float _timeAfterLastSpawn;
        private int _spawned;

        public event Action AllEnemySpawned;

        public event Action<int> WaveChanged;

        public int CurrentWaveIndex => _currentWaveIndex + 1;

        private void Awake()
        {
            InitSingleton();
            Init();
            InitSpawnPoints();
            SetWave(_currentWaveIndex);
        }

        private void Update()
        {
            Spawn();
        }

        public IEnumerator NextWave()
        {
            yield return _wavesDelay;

            SetWave(++_currentWaveIndex);
            _spawned = 0;

            if (_currentWaveIndex > _bestWaveView.BestWave)
            {
                Saves.Save(Constants.BestWaveKey, _currentWaveIndex++);
                _saver.UpdateOrCreatePlayerScore(_currentWaveIndex++);
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

        public void SetWave(int index)
        {
            _currentWave = _waves[index];

            if (_currentWaveIndex > _bestWaveView.BestWave)
            {
                Saves.Save(Constants.BestWaveKey, _currentWaveIndex++);
                _saver.UpdateOrCreatePlayerScore(_currentWaveIndex++);
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

        public void SpawnEnemy()
        {
            int randomIndex = GetRandomSpawnIndex();

            int spawnPointIndex;
            if (TryGetSpawnPoint(randomIndex, out spawnPointIndex))
            {
                _unusedSpawnPoints.RemoveAt(randomIndex);
            }
            else
            {
                ResetSpawnPoints();
                randomIndex = GetRandomSpawnIndex();
                TryGetSpawnPoint(randomIndex, out spawnPointIndex);
            }

            GameObject enemy = CreateEnemyObject();
            SetEnemyPositionAndRotation(enemy, spawnPointIndex);
        }

        private int GetRandomSpawnIndex()
        {
            return UnityEngine.Random.Range(0, _unusedSpawnPoints.Count);
        }

        private GameObject CreateEnemyObject()
        {
            GameObject enemy = _enemyPool.GetObject(_currentWave.EnemyPrefab);
            enemy.gameObject.SetActive(true);
            enemy.GetComponent<EnemyHealth>().Dying += OnEnemyDying;
            return enemy;
        }

        private void SetEnemyPositionAndRotation(GameObject enemy, int spawnPointIndex)
        {
            enemy.transform.position = _spawnPoints[spawnPointIndex].position;
            enemy.transform.rotation = _spawnPoints[spawnPointIndex].rotation;
            enemy.GetComponent<EnemyStateMachine>().SetTarget(_target);
        }

        private void OnEnemyDying(EnemyHealth enemy)
        {
            if (enemy == null)
            {
                return;
            }

            if (enemy.TryGetComponent<ExperienceEnemyProvider>(out ExperienceEnemyProvider experience))
            {
                _experienceHandler.AddExperience(experience.ExperienceForEnemy);
            }

            enemy.Dying -= OnEnemyDying;
        }

        private void ResetSpawnPoints()
        {
            _unusedSpawnPoints.Clear();

            for (int i = 0; i < _spawnPoints.Length; i++)
                _unusedSpawnPoints.Add(i);
        }

        private void Init()
        {
            _saver = GetComponent<LeaderboardSaver>();
            _experienceHandler = GetComponent<ExperienceHandler>();
            _wavesDelay = new WaitForSeconds(_delayBetweenWaves);
            _target = _playerUnit.GetComponent<IDamageable>();
            WaveChanged?.Invoke(CurrentWaveIndex);
        }

        private void InitSpawnPoints()
        {
            _unusedSpawnPoints = new List<int>();
            for (int i = 0; i < _spawnPoints.Length; i++)
            {
                _unusedSpawnPoints.Add(i);
            }
        }

        private void Spawn()
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

        private void InitSingleton()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}