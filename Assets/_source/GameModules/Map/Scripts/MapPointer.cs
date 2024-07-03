using Player;
using System;
using TinyDI;
using TinyGame;
using UnityEngine;
using Road;

namespace Map
{

    [Serializable]
    public sealed class MapPointer : IGameUpdateListener, IGameStartListener
    {
        [SerializeField]
        private Transform _mapPointerMarker;

        [SerializeField]
        private float _movementSpeed = 1;

        private MapPointsManager _mapPointsManager;

        private Transform _currentTarget;

        [SerializeField]
        private Vector3 _targetLocation;

        private int _currentTargetId;

        private bool _isDestinationReached = false;

        public float ExecutionPriority => (int)LoadingPriority.Low;

        private PlayerStats _playerStats;

        private GameManager _gameManager;

        private CheckPointView _checkPointView;

        private PauseMenuController _pauseMenuController;

        private ObstacleSpawner _obstacleSpawner;

        [Inject]
        private void Construct(MapPointsManager mapPointsManager, PlayerStats playerStats, GameManager gameManager, CheckPointView checkPointView, PauseMenuController pauseMenuController, ObstacleSpawner obstacleSpawner)
        {
            Debug.Log("Construct Map Pointer");
            _playerStats = playerStats;
            _mapPointsManager = mapPointsManager;
            _gameManager = gameManager;
            _checkPointView = checkPointView;
            _pauseMenuController = pauseMenuController;
            _obstacleSpawner = obstacleSpawner;
            _mapPointerMarker.position = _mapPointsManager.GetPoint(playerStats.lastCheckpointId).position;
            _currentTargetId = playerStats.lastCheckpointId + 1;
            SetupNextTarget();
        }

        public void ResetPointer()
        {
            _mapPointerMarker.position = _mapPointsManager.GetPoint(_playerStats.lastCheckpointId).position;
            _currentTargetId = _playerStats.lastCheckpointId + 1;
            SetupNextTarget();
            _playerStats.OnCheckpointRestart();
            _gameManager.ResumeGame();
        }

        private bool SetupNextTarget()
        {
            Transform newTarget = _mapPointsManager.GetPoint(_currentTargetId);
            if (newTarget == null)
            {
                //WIN GAME
                Debug.Log("Win Game");
                //_isGameWin = true;
                //_checkPointView.ShowWinPanel();
                _isDestinationReached = true;
                return false;
            }

            _currentTarget = newTarget;
            return true;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_isDestinationReached)
            {
                _checkPointView.ShowWinPanel();
                return;
            }
            float currentLeap = _movementSpeed * deltaTime;
            if (currentLeap > (_mapPointerMarker.position - _currentTarget.position).magnitude)
            {

                //Activate point
                MapPointController mapPointController = _currentTarget.GetComponent<MapPointController>();
                if (mapPointController != null)
                {
                    mapPointController.MapPoint.ActivatePoint(_playerStats,_currentTargetId,_checkPointView);
                    _obstacleSpawner.SetDifficulty(_playerStats.lastCheckpointId);
                }

                _mapPointerMarker.position = _currentTarget.position;
                _currentTargetId++;
                SetupNextTarget();
            }
            else
            {
                _mapPointerMarker.Translate((_currentTarget.position - _mapPointerMarker.position).normalized*currentLeap);
            }
        }

        public void OnGameStart()
        {
            _mapPointerMarker.position = _mapPointsManager.GetPoint(_playerStats.lastCheckpointId).position;
            _currentTargetId = _playerStats.lastCheckpointId + 1;
            _obstacleSpawner.SetDifficulty(_playerStats.lastCheckpointId);
            SetupNextTarget();
        }
    }
}
