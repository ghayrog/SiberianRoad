using Player;
using UnityEngine;

namespace Map
{
    public sealed class CheckPoint : IMapPoint
    {
        private string _title;
        private string _description;
        private AudioClip _sound;
        private AudioClip _music;

        public CheckPoint(string title, string description, AudioClip sound, AudioClip music)
        {
            _title = title;
            _description = description;
            _sound = sound;
            _music = music;
        }

        public void ActivatePoint(PlayerStats playerStats, int mapPointId, CheckPointView checkPointView)
        {
            //Debug.Log("Check point activated");

            checkPointView.ShowCheckpointPanel(_title, _description, _sound, _music);
            
            playerStats.UpdateCheckpointId(mapPointId);
        }
    }
}
