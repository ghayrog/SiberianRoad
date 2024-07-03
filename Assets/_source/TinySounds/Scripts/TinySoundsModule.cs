using TinyDI;
using TinyGame;
using TinyInspector;
using UnityEngine;

namespace TinySounds
{
    public class TinySoundsModule : MonoBehaviour
    {
        [SerializeField,Service(typeof(SoundSource))]
        private SoundSource _soundSource;

        [SerializeField,Service(typeof(MusicSource))]
        private MusicSource _musicSource;

        [SerializeField]
        private AudioClip[] _musicClips;

        [Inject]
        private void Construct(GameSettings gameSettings)
        {
            Debug.Log("Construct TinySoundsModule");
            _musicSource.Initialize(gameSettings, _musicClips[0]);
            _soundSource.Initialize(gameSettings);
        }

        private void Update()
        {
            _musicSource.Update(Time.deltaTime);
        }

        private void OnDisable()
        {
            _musicSource.OnDisable();
            _soundSource.OnDisable();
        }
    }
}
