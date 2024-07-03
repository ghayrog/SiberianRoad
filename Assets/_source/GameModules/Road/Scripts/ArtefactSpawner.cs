using System.Collections.Generic;
using TinyGame;
using UnityEngine;

namespace Road
{
    public class ArtefactSpawner : MonoBehaviour, IGameUpdateListener, IGameStartListener, IGameFinishListener
    {
        private const int ARTEFACT_TYPE_NUMBER = 3;

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
        private ObjectPool _rabbitPool;

        [SerializeField]
        private ObjectPool _owlPool;

        [SerializeField]
        private ObjectPool _foxPool;

        private List<Artefact> _artefacts = new();

        public float ExecutionPriority => (int)LoadingPriority.Low;

        private float _spawnTimer = 0;

        private bool _isSpawninAllowed = false;

        public void AllowSpawn()
        {
            _isSpawninAllowed = true;
        }

        public void DisableSpawn()
        {
            _isSpawninAllowed = false;
        }

        public void SpawnArtefact()
        {
            int randomId = Random.Range(0, _yPositions.Length);
            Vector3 spawnPoint = new Vector3(_spawnPoint.position.x, _yPositions[randomId], _spawnPoint.position.z);

            ArtefactType randomObstacle = (ArtefactType)Random.Range(0, ARTEFACT_TYPE_NUMBER);

            Artefact newObstacle = null;

            switch (randomObstacle)
            {
                case ArtefactType.Rabbit:
                    newObstacle = _rabbitPool.GetFromPool().GetComponent<Artefact>();
                    break;
                case ArtefactType.Owl:
                    newObstacle = _owlPool.GetFromPool().GetComponent<Artefact>();
                    break;
                case ArtefactType.Fox:
                    newObstacle = _foxPool.GetFromPool().GetComponent<Artefact>();
                    break;
                default:
                    break;
            }

            if (newObstacle != null)
            {
                newObstacle.gameObject.transform.position = spawnPoint;
                newObstacle.Initialize(_speed + ((float)randomId - 0.5f), randomId * 10 - 5);
                _artefacts.Add(newObstacle);
            }

        }

        public void OnGameFinish()
        {
            _artefacts = new();
            _rabbitPool.CleanPool();
            _owlPool.CleanPool();
            _foxPool.CleanPool();
            _isSpawninAllowed = false;
        }

        public void OnGameStart()
        {
            _artefacts = new();
            _rabbitPool.InitializePool();
            _owlPool.InitializePool();
            _foxPool.InitializePool();
            _isSpawninAllowed = true;
            _spawnTimer = _spawnInterval;
        }

        public void DestroyArtefact(Artefact artefact)
        {
            _artefacts.Remove(artefact);
            switch (artefact.ArtefactType)
            {
                case ArtefactType.Rabbit:
                    _rabbitPool.ReturnToPool(artefact.gameObject);
                    break;
                case ArtefactType.Owl:
                    _owlPool.ReturnToPool(artefact.gameObject);
                    break;
                case ArtefactType.Fox:
                    _foxPool.ReturnToPool(artefact.gameObject);
                    break;
                default:
                    break;
            }
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var artefact in _artefacts)
            {
                artefact.OnUpdate(deltaTime);
            }

            for (int i = 0; i < _artefacts.Count; i++)
            {
                Artefact artefact = _artefacts[i];
                if (artefact.gameObject.transform.position.x < _destructPoint.position.x)
                {
                    _artefacts.RemoveAt(i);
                    switch (artefact.ArtefactType)
                    {
                        case ArtefactType.Rabbit:
                            _rabbitPool.ReturnToPool(artefact.gameObject);
                            break;
                        case ArtefactType.Owl:
                            _owlPool.ReturnToPool(artefact.gameObject);
                            break;
                        case ArtefactType.Fox:
                            _foxPool.ReturnToPool(artefact.gameObject);
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
                SpawnArtefact();
            }

        }
    }
}
