using System;
using TinyDI;
using TinyInspector;
using TinySave;
using UnityEngine;

namespace TinySaveLoaders
{
    internal sealed class GameProgressManagerInstaller : MonoBehaviour
    {
        [Service(typeof(GameProgressAdapter))]
        private GameProgressAdapter _gameProgressAdapter = new();

        [Service(typeof(GameProgressManager))]
        private GameProgressManager _gameProgressManager = new();

        [Inject]
        private void Construct(GameProgressManager gameProgressManager)
        {
            _gameProgressManager = gameProgressManager;
            //_gameProgressManager.AddSaveLoader();
        }

        [TinyPlayButton]
        internal void Save()
        {
            _gameProgressManager.SaveProgress();
        }

        [TinyPlayButton]
        internal void Load()
        {
            _gameProgressManager.LoadProgress();
        }

        [TinyPlayButton]
        private void DeleteSave()
        {
            _gameProgressManager.DeleteSave();
        }
    }
}
