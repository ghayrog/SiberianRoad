using System;
using TinyDI;
using TinyGame;
using UnityEngine;

namespace TinySaveLoaders
{
    [Serializable]
    public sealed class AutoSaver : IGameUpdateListener
    {
        [SerializeField]
        private float _autoSaveTimerSeconds;

        private float _elapsedTime = 0;

        private GameProgressManager _gameProgressManager;

        public float ExecutionPriority => (int)LoadingPriority.Low;

        [Inject]
        private void Construct(GameProgressManager gameProgressManager)
        { 
            _gameProgressManager = gameProgressManager;
        }

        public void AutoSaveGame()
        {
            _gameProgressManager.SaveProgress();
        }

        public void OnUpdate(float deltaTime)
        {
            _elapsedTime += deltaTime;

            if (_autoSaveTimerSeconds > 0 && _elapsedTime >= _autoSaveTimerSeconds)
            {
                _elapsedTime = 0;
                AutoSaveGame();
            }
        }
    }
}
