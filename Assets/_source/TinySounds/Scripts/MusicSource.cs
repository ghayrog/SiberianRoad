using System;
using TinyGame;
using UnityEngine;

namespace TinySounds
{
    [Serializable]
    public sealed class MusicSource
    {
        [SerializeField]
        private AudioSource _audioSource1;

        [SerializeField]
        private AudioSource _audioSource2;

        [SerializeField]
        private float _crossfadeDuration = 3.0f;

        private float _crossfadeSpeed => (_targetVolume - 0) / _crossfadeDuration;

        private AudioSource _currentAudioSource;

        private float _targetVolume;

        private GameSettings _gameSettings;

        public void SetVolume(float volume)
        {
            _targetVolume = volume;
        }

        public void Initialize(GameSettings gameSettings, AudioClip audioClip)
        {
            _gameSettings = gameSettings;
            _targetVolume = gameSettings.MusicVolume;
            _currentAudioSource = _audioSource1;
            _currentAudioSource.clip = audioClip;
            _currentAudioSource.Play();
            _currentAudioSource.loop = true;
            _currentAudioSource.volume = gameSettings.MusicVolume;

            _audioSource2.loop = true;
            _audioSource2.volume = 0;

            _gameSettings.OnGameSettingsChange += OnSettingsChangedHandler;
        }

        public void OnSettingsChangedHandler()
        {
            SetVolume(_gameSettings.MusicVolume);
        }

        public void ChangeSoundTrackImmediately(AudioClip audioClip)
        { 
            _audioSource1.Stop();
            _audioSource2.Stop();
            _currentAudioSource = _audioSource1;
            _currentAudioSource.clip = audioClip;
            _currentAudioSource.Play();
            _currentAudioSource.loop = true;
            _currentAudioSource.volume = _targetVolume;

            _audioSource2.volume = 0;
        }

        public void ChangeSoundTrackCrossfade(AudioClip audioClip)
        {
            _currentAudioSource = (_audioSource1 == _currentAudioSource)?_audioSource2 : _audioSource1;
            _currentAudioSource.clip = audioClip;
            _currentAudioSource.Play();
            _currentAudioSource.loop = true;
        }

        public void Update(float deltaTime)
        {
            AudioSource secondarySource = (_audioSource1 == _currentAudioSource) ? _audioSource2 : _audioSource1;
            
            float primaryVolume = _currentAudioSource.volume + _crossfadeSpeed * deltaTime;
            primaryVolume = Mathf.Min(primaryVolume, _targetVolume);
            _currentAudioSource.volume = primaryVolume;

            float secondaryVolume = secondarySource.volume - _crossfadeSpeed * deltaTime;
            secondaryVolume = Mathf.Max(secondaryVolume, 0);
            secondarySource.volume = secondaryVolume;
        }

        public void OnDisable()
        {
            _gameSettings.OnGameSettingsChange -= OnSettingsChangedHandler;
        }
    }
}
