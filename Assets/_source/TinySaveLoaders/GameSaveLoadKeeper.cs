using TinyDI;
using TinyGame;
using UnityEngine;

namespace TinySaveLoaders
{
    internal sealed class GameSaveLoadKeeper
    {
        private GameProgressManager _gameProgressManager;

        [Inject]
        private void Construct(GameProgressManager gameProgressManager)
        {
            _gameProgressManager = gameProgressManager;

            gameProgressManager.AddSaveLoader(new UnitPositionsSaveLoader());
            gameProgressManager.AddSaveLoader(new PlayerStatsSaveLoader());
            //Add SaveLoaders here

            LoadProgress();

            Debug.Log("Initializing Save/Load system OK");
        }

        private void LoadProgress()
        {
            GlobalContext globalContext = GameObject.FindObjectOfType<GlobalContext>().GetComponent<GlobalContext>();
            GlobalGameProperties properties = globalContext.Resolve<GlobalGameProperties>();
            if (properties.IsGameContinued)
            {
                _gameProgressManager.LoadProgress();
            }
        }
    }
}
