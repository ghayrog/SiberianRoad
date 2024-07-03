using System;
using UnityEngine;
using UnityEngine.UI;
using TinyDI;

namespace TinyGame
{
    [Serializable]
    public sealed class GameLoaderAdapter
    {
        [SerializeField]
        private GrayableButton _startButton;

        [SerializeField]
        private GrayableButton _continueButton;

        [SerializeField]
        private LoadingLogo _loadingLogo;

        private GameLoader _gameLoader;

        private GlobalGameProperties _gameProperties;

        [Inject]
        public void Construct(GameLoader gameLoader, GlobalGameProperties gameProperties)
        {
            _gameLoader = gameLoader;
            _gameLoader.OnGameLoaded += OnGameLoadedHandler;
            _gameLoader.OnSceneLoading += Unsubscribe;
            _startButton.Button.onClick.AddListener(OnStartButtonClickHandler);
            _continueButton?.Button.onClick.AddListener(OnContinueButtonClickHandler);
            _startButton.GrayOut();
            _continueButton?.GrayOut();
            _loadingLogo?.Show();

            //Debug.Log("Null Ref:");
            _gameProperties = gameProperties; // GlobalContext.Instance.Resolve<GlobalGameProperties>();
            //Debug.Log("Null Ref!");
        }

        private void OnStartButtonClickHandler()
        {
            if (_gameLoader.TryLoadGame())
            {
                _gameProperties.IsGameContinued = false;
            }
        }

        private void OnContinueButtonClickHandler()
        {
            if (_gameLoader.TryLoadGame())
            {
                _gameProperties.IsGameContinued = true;
            }
        }

        private void OnGameLoadedHandler()
        {
            _startButton.ActivateButton();
            _loadingLogo?.Hide();
            //Debug.Log(_gameLoader.IsGameContinueAllowed);
            if (_gameLoader.IsGameContinueAllowed)
            { 
                _continueButton?.ActivateButton();
            }
        }

        public void Unsubscribe()
        {
            _gameLoader.OnGameLoaded -= OnGameLoadedHandler;
            _startButton.Button.onClick.RemoveListener(OnStartButtonClickHandler);
            _continueButton?.Button.onClick.RemoveListener(OnContinueButtonClickHandler);

            _gameLoader.OnSceneLoading -= Unsubscribe;
        }


    }
}
