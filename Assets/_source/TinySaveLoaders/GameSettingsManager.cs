using System;
using System.Collections;
using TinyDI;
using TinyGame;
using TinyInspector;
using TinySave;
using TinySounds;
using UnityEngine;

namespace TinySaveLoaders
{
    internal sealed class GameSettingsManager : MonoBehaviour
    {
        private ISaveLoader _settingsSaveLoader;

        private IGameRepository _settingsRepository;

        private DIContext _settingsContext;

        [SerializeField]
        private float _autoSaveTimeoutSeconds;

        [SerializeField, Service(typeof(GameSettings))]
        private GameSettings _gameSettings;

        [SerializeField, Service(typeof(GameSettingsAdapter))]
        private GameSettingsAdapter _gameSettingsAdapter;

        private SoundSource _soundSource;

        [Inject]
        private void Construct(DIContext dIContext, SoundSource soundSource)
        {
            //Debug.Log("Game settings manager constructed");
            _settingsContext = dIContext;
            _soundSource = soundSource;
        }

        private DateTime _previousTimeStamp = DateTime.MinValue;

        private void OnSettingsChangedHandler()
        {
            DateTime currentTimeStamp = DateTime.Now;
            if (_autoSaveTimeoutSeconds > 0 && currentTimeStamp > _previousTimeStamp.AddSeconds(_autoSaveTimeoutSeconds))
            {
                Invoke("SaveSettings", _autoSaveTimeoutSeconds);
                _previousTimeStamp = currentTimeStamp;
            }
        }

        private void OnEnable()
        {
            _settingsSaveLoader = new GameSettingsSaveLoader();
            _settingsRepository = new PrefsGameRepository();

            LoadSettings();
            _gameSettingsAdapter.Subscribe();
            _gameSettings.OnGameSettingsChange += OnSettingsChangedHandler;
        }

        [TinyPlayButton]
        private void SaveSettings()
        {
            _settingsSaveLoader.SaveGame(_settingsRepository, _settingsContext);
            _settingsRepository.SaveState();
            _soundSource?.PlayDefaultSound();
        }

        [TinyPlayButton]
        private void LoadSettings()
        {
            _settingsRepository.LoadState();
            _settingsSaveLoader.LoadGame(_settingsRepository, _settingsContext);
        }

        [TinyButton]
        private void DeleteSettings()
        {
            _settingsRepository.DeleteRepository();
        }

        private void OnDisable()
        {
            _gameSettingsAdapter.Unsubscribe();
            _gameSettings.OnGameSettingsChange -= OnSettingsChangedHandler;
        }
    }
}
