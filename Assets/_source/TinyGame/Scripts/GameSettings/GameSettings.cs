using System;
using UnityEngine;

namespace TinyGame
{
    [Serializable]
    public sealed class GameSettings
    {
        public Action OnGameSettingsChange;

        [field:SerializeField]
        public float MusicVolume { get; private set; }

        [field:SerializeField]
        public float SoundVolume { get; private set; }

        public void SetVolumes(float sound, float music)
        {
            //Debug.Log($"Set Volumes: {sound}, {music}");
            SoundVolume = sound;
            MusicVolume = music;
            OnGameSettingsChange?.Invoke();
        }
    }

}
