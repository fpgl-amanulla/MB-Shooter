using System;
using System.Collections.Generic;
using _Game.Scripts.Runtime.Controllers;
using Lean.Pool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Game.Managers
{
    public class EnemyWaveManager : MonoBehaviour
    {
        [SerializeField] private EnemyController _enemyPrefab;
        [SerializeField] private float _spawnDelay = .25f;

        [SerializeField] private List<Transform> _spawnPointList = new List<Transform>();

        private bool _isGameStarted;

        private float _timeCount;

        private void Start()
        {
            GameManager.Instance.OnLevelStart += OnLevelStart;
        }

        private void Update()
        {
            if (!_isGameStarted) return;
            if (GameManager.Instance.IsGameOver) return;
            
            _timeCount += Time.deltaTime;
            if (_timeCount > _spawnDelay)
            {
                _timeCount = 0;
                SpawnEnemy();
            }
        }

        private void SpawnEnemy()
        {
            Instantiate(_enemyPrefab, _spawnPointList[Random.Range(0, _spawnPointList.Count)].position, _enemyPrefab.transform.rotation);
        }

        private void OnDestroy()
        {
            GameManager.Instance.OnLevelStart -= OnLevelStart;
        }

        private void OnLevelStart() => _isGameStarted = true;
    }
}