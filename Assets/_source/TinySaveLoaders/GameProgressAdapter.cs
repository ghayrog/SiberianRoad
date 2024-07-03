using System;
using TinyDI;
using TinyGame;

namespace TinySaveLoaders
{
    [Serializable]
    internal sealed class GameProgressAdapter
    {
        private GameProgressManager _gameProgressManager;

        private GameLoader _gameLoader;

        [Inject]
        private void Construct(GameProgressManager gameProgressManager, GameLoader gameLoader)
        {
            //UnityEngine.Debug.Log("Construct Adapter");
            _gameLoader = gameLoader;
            _gameProgressManager = gameProgressManager;
            _gameProgressManager.OnSaveFound += OnSaveFoundHandler;
            _gameLoader.OnSceneLoading += Unsubscribe;
        }

        private void OnSaveFoundHandler()
        {
            _gameLoader.AllowContinue();
        }

        private void Unsubscribe()
        {
            _gameProgressManager.OnSaveFound -= OnSaveFoundHandler;
            _gameLoader.OnSceneLoading -= Unsubscribe;
        }
    }
}
