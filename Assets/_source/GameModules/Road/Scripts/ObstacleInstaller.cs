using TinyDI;
using TinyGame;
using UnityEngine;

namespace Road
{
    public sealed class ObstacleInstaller : MonoBehaviour
    {
        [SerializeField, Listener, Service(typeof(ObstacleSpawner))]
        private ObstacleSpawner _obstacleSpawner;

        [SerializeField, Listener, Service(typeof(ArtefactSpawner))]
        private ArtefactSpawner _artefactSpawner;

    }
}
