using UnityEngine;
using TinyDI;
using System.Collections.Generic;

namespace TinyGame
{
    public sealed class GlobalContext : MonoBehaviour
    {
        public static GlobalContext Instance;

        [SerializeField,Service(typeof(GlobalGameProperties))]
        private GlobalGameProperties _gameProperties;

        [SerializeField]
        private MonoBehaviour[] _globalModules;

        private DIContext _globalContext;        

        public T Resolve<T>() where T : class
        {
            return _globalContext.Resolve<T>();
        }

        private bool _isInitialized = false;

        public IEnumerable<MonoBehaviour> ExtractAllModules()
        {
            for (int i = 0; i < _globalModules.Length; i++)
            { 
                yield return _globalModules[i];
            }
        }

        private void Awake()
        {
            GlobalContext[] objectClones = GameObject.FindObjectsOfType<GlobalContext>();
            if (objectClones.Length > 1)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            Initialize();
            DontDestroyOnLoad(gameObject);
        }

        public void Initialize()
        {
            if (_isInitialized) return;

            _globalContext = new DIContext();
            _globalContext.AddModule(this);

            foreach (var module in _globalModules)
            {
                _globalContext.AddModule(module);
            }

            _globalContext.InstallServices();

            _isInitialized = true;
            Debug.Log("Global context initialized");
        }

    }
}
