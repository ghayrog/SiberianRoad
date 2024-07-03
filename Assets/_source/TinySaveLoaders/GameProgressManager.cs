using System;
using System.Collections;
using System.Collections.Generic;
using TinyDI;
using TinyGame;
using TinyInspector;
using TinySave;
using UnityEngine;

namespace TinySaveLoaders
{
    [Serializable]
    public sealed class GameProgressManager
    {
        internal Action OnSaveFound;

        private List<ISaveLoader> _gameProgressSaveLoaders = new();

        private IGameRepository _gameProgressRepository;

        private DIContext _dIContext;

        internal bool IsSaveExisting { get; private set; }

        [Inject]
        internal void Construct(DIContext dIContext)
        {
            //UnityEngine.Debug.Log("Construct Manager");
            _dIContext = dIContext;

            _gameProgressRepository = new FileGameRepository();
            LoadStateIntoRepository();
        }

        internal void AddSaveLoader(ISaveLoader saveLoader)
        { 
            _gameProgressSaveLoaders.Add(saveLoader);
        }

        internal void LoadStateIntoRepository()
        {
            IsSaveExisting = _gameProgressRepository.LoadState();
            if (IsSaveExisting) OnSaveFound?.Invoke();
        }

        
        public void SaveProgress()
        {
            Debug.Log("Saving progress...");
            foreach (ISaveLoader saveLoader in _gameProgressSaveLoaders)
            {
                saveLoader.SaveGame(_gameProgressRepository, _dIContext);
            }
            _gameProgressRepository.SaveState();
        }

        
        public void LoadProgress()
        {
            Debug.Log("Loading progress...");
            _gameProgressRepository.LoadState();
            foreach (ISaveLoader saveLoader in _gameProgressSaveLoaders)
            {
                saveLoader.LoadGame(_gameProgressRepository, _dIContext);
            }
        }

        
        internal void DeleteSave()
        {
            _gameProgressRepository.DeleteRepository();
        }
    }
}
