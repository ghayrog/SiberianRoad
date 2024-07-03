using TinyDI;
using TinyGame;
using TinyInspector;
using UnityEngine;

namespace TinySaveLoaders
{
    internal sealed class GameSaveLoadInstaller : MonoBehaviour
    {
        [Service(typeof(GameProgressManager))]
        private GameProgressManager _gameProgressManager = new();

        [SerializeField, Listener, Service(typeof(AutoSaver))]
        private AutoSaver _autoSaver;

        [Service(typeof(GameSaveLoadKeeper))]
        private GameSaveLoadKeeper _gameSaveLoadKeeper = new();

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
