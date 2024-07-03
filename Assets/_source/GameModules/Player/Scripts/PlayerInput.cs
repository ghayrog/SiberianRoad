using System;
using TinyDI;
using TinyGame;
using UnityEngine;

namespace Player
{
    [Serializable]
    public sealed class PlayerInput : IGameUpdateListener
    {
        private const string AXIS_NAME = "Vertical";

        public float ExecutionPriority => (int)LoadingPriority.Low;

        private PlayerController _playerControler;

        [Inject]
        private void Construct(PlayerController playerController)
        { 
            _playerControler = playerController;
        }

        public void OnUpdate(float deltaTime)
        {
            if (Input.GetAxisRaw(AXIS_NAME) < 0)
            {
                _playerControler.IncreasePosition();
            }
            if (Input.GetAxisRaw(AXIS_NAME) > 0)
            { 
                _playerControler.DecreasePosition();
            }
        }
    }
}
