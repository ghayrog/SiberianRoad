using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using TinyDI;

namespace TinyGame
{
    public sealed class SceneContext : MonoBehaviour
    {
        [SerializeField,Service(typeof(GameManager))]
        private GameManager _gameManager;

        [SerializeField]
        private MonoBehaviour[] _gameModules;

        [Service(typeof(DIContext))]
        private DIContext _gameContext;

        [Service(typeof(GlobalContext))]
        private GlobalContext _globalContext;

        private void Awake()
        {
            _gameContext = new DIContext();
            _globalContext = FindObjectOfType<GlobalContext>().GetComponent<GlobalContext>();
            _gameContext.AddModule(this);
            foreach (var module in _gameModules)
            {
                _gameContext.AddModule(module);
            }

            //GlobalContext.Instance.Initialize();
            _gameContext.Initialize();

            AddListenersToGameManager();
        }

        private void AddListenersToGameManager()
        {
            foreach (var module in _gameModules)
            {
                Debug.Log($"Adding multiple listeners from module {module} as a custom Listener Provider");
                _gameManager.AddMultipleListeners(ProvideListeners(module));
                if (module is IGameListener tListener)
                {
                    Debug.Log($"Adding single listener from module {module}");
                    _gameManager.AddListener(tListener);
                }
            }

        }

        public IEnumerable<IGameListener> ProvideListeners(object module)
        {
            var fields = ReflectionTools.GetFields(module);

            foreach (var field in fields)
            {
                if (field.IsDefined(typeof(ListenerAttribute)) &&
                    field.GetValue(module) is IGameListener gameListener)
                {
                    yield return gameListener;
                }
            }
        }
    }
}
