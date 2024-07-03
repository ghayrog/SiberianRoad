using System;
using TinyGame;
using TinyInspector;
using UnityEngine;

namespace Player
{
    [Serializable]
    public class PlayerController : IGameUpdateListener, IGameStartListener
    {
        [SerializeField]
        private float[] _yPositions;

        [SerializeField]
        private Transform _player;

        [SerializeField]
        private SpriteRenderer _playerSprite;

        [SerializeField]
        private Transform _wagon;

        [SerializeField]
        private Transform _horses;

        [SerializeField]
        private float _strafeSpeed;

        [SerializeField]
        private int _defaultPositionId;

        private int _currentPositionId;

        public float ExecutionPriority => (int)LoadingPriority.Low;

        public void OnGameStart()
        {
            _currentPositionId = _defaultPositionId;
            _playerSprite.sortingOrder = _currentPositionId * 2 + 1;
            _player.position = new Vector3(_player.position.x, _yPositions[_defaultPositionId], _player.position.z);
        }

        public void OnUpdate(float deltaTime)
        {
            float direction = Mathf.Sign(_yPositions[_currentPositionId] - _player.position.y);
            if (direction == 0)
            {
                return;
            }
            float leap = _strafeSpeed * Time.deltaTime;
            if (Mathf.Abs(_yPositions[_currentPositionId] - _player.position.y) < leap)
            {
                _player.position = new Vector3(_player.position.x, _yPositions[_currentPositionId], _player.position.z);
            }
            else
            {
                _player.Translate(new Vector3(0,leap * direction,0));
            }
        }

        public void IncreasePosition()
        {
            if (_yPositions[_currentPositionId] != _player.position.y)
            {
                return;
            }
            if (_currentPositionId < _yPositions.Length - 1)
            { 
                _currentPositionId++;
                _playerSprite.sortingOrder = _currentPositionId * 2 + 1;
            }
        }

        public void DecreasePosition()
        {
            if (_yPositions[_currentPositionId] != _player.position.y)
            {
                return;
            }
            if (_currentPositionId >0)
            {
                _currentPositionId--;
                _playerSprite.sortingOrder = _currentPositionId * 2 + 1;
            }

        }
    }
}
