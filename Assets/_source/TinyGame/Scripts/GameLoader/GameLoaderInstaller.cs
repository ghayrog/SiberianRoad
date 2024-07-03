using UnityEngine;
using TinyDI;

namespace TinyGame
{
    internal sealed class GameLoaderInstaller : MonoBehaviour
    {
        [SerializeField, Service(typeof(GameLoader))]
        private GameLoader _gameLoader;

        [SerializeField, Service(typeof(GameLoaderAdapter))]
        private GameLoaderAdapter _gameLoaderAdapter;

        [SerializeField, Service(typeof(MenuPanelsAdapter))]
        private MenuPanelsAdapter _menuPanelsAdapter;

        [SerializeField]
        private GameExit _gameExit;

        [SerializeField]
        private MonoBehaviour[] _gameLoaderModules;

        [Service(typeof(DIContext))]
        private DIContext _loaderContext;

        private void Awake()
        {
            _loaderContext = new DIContext();
            _loaderContext.AddModule(this);

            foreach (var module in _gameLoaderModules)
            {
                _loaderContext.AddModule(module);
            }
            //GlobalContext.Instance.Initialize();
            _loaderContext.Initialize();
            _menuPanelsAdapter.Initialize();
            _gameExit.Initialize();
        }

        private void OnDisable()
        {
            _menuPanelsAdapter.Unsubscribe();
            _gameExit.Unsubscribe();
        }
    }
}
