using System;
using TinyDI;
using UnityEngine;
using UnityEngine.UI;

namespace TinyGame
{
    [Serializable]
    public sealed class GameSettingsAdapter
    {
        [SerializeField]
        private Slider _soundSlider;

        [SerializeField]
        private Slider _musicSlider;

        private GameSettings _gameSettings;

        [Inject]
        private void Construct(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
        }

        private void UpdateGameSettings(float value)
        {
            //Debug.Log($"Update Game Settings {value}");
            _gameSettings.SetVolumes(_soundSlider.value, _musicSlider.value);
        }

        private void UpdateSliderValues()
        {
            _soundSlider.value = _gameSettings.SoundVolume;
            _musicSlider.value = _gameSettings.MusicVolume;
        }

        public void Subscribe()
        {
            UpdateSliderValues();
            _soundSlider.onValueChanged.AddListener(UpdateGameSettings);
            _musicSlider.onValueChanged.AddListener(UpdateGameSettings);
        }

        public void Unsubscribe()
        {
            _soundSlider.onValueChanged.RemoveListener(UpdateGameSettings);
            _musicSlider.onValueChanged.RemoveListener(UpdateGameSettings);
        }
    }
}
