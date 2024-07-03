using System;
using TinyGame;
using UnityEngine;

namespace TinySounds
{
    [Serializable]
    public sealed class SoundSource
    {
        private const float CACHE_PERIOD = 0.5f;

        [SerializeField]
        public AudioSource audioSource;

        [SerializeField]
        private AudioClip _defaultClip;

        private GameSettings _gameSettings;

        private float _volumeCache;

        public void Initialize(GameSettings gameSettings)
        { 
            _gameSettings = gameSettings;
            audioSource.volume = _gameSettings.SoundVolume;
            _gameSettings.OnGameSettingsChange += OnSettingsChangedHandler;
        }

        public void OnSettingsChangedHandler()
        {
            audioSource.volume = _gameSettings.SoundVolume;
        }

        public void PlayDefaultSound()
        { 
            audioSource?.PlayOneShot(_defaultClip);
        }

        public void PlaySound(AudioClip clip)
        {
            audioSource?.PlayOneShot(clip);
        }

        public void OnDisable()
        {
            _gameSettings.OnGameSettingsChange -= OnSettingsChangedHandler;
        }
    }
}
