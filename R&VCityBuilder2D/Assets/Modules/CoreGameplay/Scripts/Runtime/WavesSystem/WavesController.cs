using System;
using System.Collections.Generic;
using Modules.EntitySystem.Scripts.Runtime;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public enum WaveState
{
    WaitingToSpawnNextWave,
    SpawningWave,
}

namespace Modules.CoreGameplay.Scripts.Runtime.WavesSystem
{
    public class WavesController : MonoBehaviour
    {
        public event EventHandler OnWaveNumberChanged;
        
        [SerializeField] private EnemyPool _enemyPool;
        [SerializeField] private List<Transform> _spawnPositionList;
        [SerializeField] private Transform _nextWaveSpawnPosition;
        [SerializeField] private int _spawnAmount;

        private WaveState _waveState;
        private float nextWaveSpawnTimer; 
        private float nextEnemySpawnTimer; 
        private int remainingEnemySpawnAmount;
        private Vector3 spawnPosition;
        private int _waveNumber;
        private void Awake() => _enemyPool ??= FindAnyObjectByType<EnemyPool>();
        private void Start()
        {
            _waveState = WaveState.WaitingToSpawnNextWave;
            spawnPosition = _spawnPositionList[Random.Range(0, _spawnPositionList.Count)].position;
            _nextWaveSpawnPosition.position = spawnPosition;
            nextWaveSpawnTimer = 3f;
        }

        private void Update()
        {
            ManageWaves();
        }

        private void ManageWaves()
        {
            switch (_waveState)
            {
                case WaveState.WaitingToSpawnNextWave:
                    nextWaveSpawnTimer -= Time.deltaTime;
                    if (nextWaveSpawnTimer < 0f)
                        SpawnWave();
                    break;
                case WaveState.SpawningWave:
                    if (remainingEnemySpawnAmount > 0)
                    {
                        nextEnemySpawnTimer -= Time.deltaTime;
                        if (nextEnemySpawnTimer < 0f)
                        {
                            nextEnemySpawnTimer = Random.Range(0f, 0.2f);
                            var getNewEnemy = _enemyPool.GetObject();
                            getNewEnemy.GetTransform().position = spawnPosition * Random.Range(3f,5f);
                            remainingEnemySpawnAmount--;

                            if (remainingEnemySpawnAmount <= 0f)
                            {
                                _waveState = WaveState.WaitingToSpawnNextWave;
                                spawnPosition = _spawnPositionList[Random.Range(0, _spawnPositionList.Count)].position;
                                _nextWaveSpawnPosition.position = spawnPosition;
                                nextWaveSpawnTimer = 10f;
                            }
                        }
                    }
                    break;
            }
        }

        private void SpawnWave()
        {
            remainingEnemySpawnAmount = _spawnAmount + 3 * _waveNumber;
            _waveNumber++;
            _waveState = WaveState.SpawningWave;
            OnWaveNumberChanged?.Invoke(this, EventArgs.Empty);
        }

        public int GetWaveNumber()
        {
            return _waveNumber;
        }

        public float GetNextWaveSpawnTimer()
        {
            return nextWaveSpawnTimer;
        }

        public Vector3 GetSpawnPosition()
        {
            return spawnPosition;
        }
    }
}