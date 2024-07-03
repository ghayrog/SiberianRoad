using System.Collections;
using System.Collections.Generic;
using TinyGame;
using UnityEngine;

namespace Road
{
    public class ObstacleSpawner : MonoBehaviour, IGameUpdateListener, IGameStartListener, IGameFinishListener
    {
        private const int OBSTACLE_TYPE_NUMBER = 2;

        [SerializeField]
        private float[] _yPositions;

        [SerializeField]
        private Transform _spawnPoint;

        [SerializeField]
        private Transform _destructPoint;

        [SerializeField]
        private float _speed;

        [SerializeField]
        private float _spawnInterval;

        [SerializeField]
        private ObjectPool _snowPool;

        [SerializeField]
        private ObjectPool _stonePool;

        private List<Obstacle> _obstacles = new();

        public float ExecutionPriority => (int)LoadingPriority.Low;

        private float _spawnTimer = 0;

        private bool _isSpawninAllowed = false;

        [SerializeField]
        private float _initialSpawnInterval;

        [SerializeField]
        private float _difficultyMultiplier;

        public void AllowSpawn()
        { 
            _isSpawninAllowed = true;
        }

        public void DisableSpawn()
        { 
            _isSpawninAllowed = false;
        }

        public void SetDifficulty(int id)
        {
            _spawnInterval = _initialSpawnInterval / ((float)(_difficultyMultiplier+id)/_difficultyMultiplier);
            Debug.Log($"Set Difficulty {id} Spawn interval: {_spawnInterval}");
        }

        public void SpawnObstsacle()
        {
            int randomId = Random.Range(0, _yPositions.Length);
            Vector3 spawnPoint = new Vector3(_spawnPoint.position.x, _yPositions[randomId], _spawnPoint.position.z);

            ObstacleType randomObstacle = (ObstacleType)Random.Range(0,OBSTACLE_TYPE_NUMBER);

            Obstacle newObstacle = null;

            switch (randomObstacle)
            {
                case ObstacleType.Stone:
                    newObstacle = _stonePool.GetFromPool().GetComponent<Obstacle>();
                    break;
                case ObstacleType.Snow:
                    newObstacle = _snowPool.GetFromPool().GetComponent<Obstacle>();
                    break;
                default:
                    break;
            }

            if (newObstacle != null)
            {
                newObstacle.gameObject.transform.position = spawnPoint;
                newObstacle.Initialize(_speed + (float)(randomId-1)*0.25f, randomId * 2);
                _obstacles.Add(newObstacle);
            }

        }

        public void OnGameFinish()
        {
            _obstacles = new();
            _snowPool.CleanPool();
            _stonePool.CleanPool();
            _isSpawninAllowed = false;
        }

        public void OnGameStart()
        {
            _obstacles = new();
            _snowPool.InitializePool();
            _stonePool.InitializePool();
            _isSpawninAllowed = true;
            _spawnTimer = _spawnInterval;
        }

        public void DestroyObstacle(Obstacle obstacle)
        {
            _obstacles.Remove(obstacle);
            switch (obstacle.ObstacleType)
            {
                case ObstacleType.Stone:
                    _stonePool.ReturnToPool(obstacle.gameObject);
                    break;
                case ObstacleType.Snow:
                    _snowPool.ReturnToPool(obstacle.gameObject);
                    break;
                default:
                    break;
            }
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var obstacle in _obstacles)
            {
                obstacle.OnUpdate(deltaTime);
            }

            for (int i = 0; i < _obstacles.Count; i++)
            {
                Obstacle obstacle= _obstacles[i];
                if (obstacle.gameObject.transform.position.x < _destructPoint.position.x)
                {
                    _obstacles.RemoveAt(i);
                    switch (obstacle.ObstacleType)
                    {
                        case ObstacleType.Stone:
                            _stonePool.ReturnToPool(obstacle.gameObject);
                            break;
                        case ObstacleType.Snow:
                            _snowPool.ReturnToPool(obstacle.gameObject);
                            break;
                        default:
                            break;
                    }
                    i--;
                }

            }

            if (!_isSpawninAllowed)
            {
                return;
            }

            _spawnTimer += deltaTime;
            if (_spawnTimer >= _spawnInterval)
            { 
                _spawnTimer = 0;
                SpawnObstsacle();
            }

        }
    }
}
