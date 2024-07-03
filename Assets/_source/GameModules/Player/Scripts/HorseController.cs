using Road;
using TinyDI;
using TinySounds;
using UnityEngine;
using TinyGame;

namespace Player
{
    public sealed class HorseController : MonoBehaviour
    {
        private const float DEFAULT_DAMAGE = 0.1f;

        private PlayerStats _playerStats;

        private ObstacleSpawner _obstacleSpawner;

        [SerializeField]
        private AudioClip _failSound;

        private GlobalContext _globalContext;

        private SoundSource _soundSource;

        [Inject]
        private void Construct(PlayerStats playerStats, ObstacleSpawner obstacleSpawner, GlobalContext globalContext)
        {
            _playerStats = playerStats;
            _obstacleSpawner = obstacleSpawner;
            _globalContext = globalContext;
            _soundSource = _globalContext.Resolve<SoundSource>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            //Debug.Log("Wagon collided with something");
            Obstacle obstacle = other.gameObject.GetComponent<Obstacle>();

            if (obstacle == null)
            {
                return;
            }
            //Debug.Log("Wagon detected an obstacle");
            ObstacleType obstacleType = obstacle.ObstacleType;

            if (obstacleType != ObstacleType.Snow)
            {
                return;
            }
            //Debug.Log("Wagon detected a stone");

            _playerStats.DecreaseHorsePower(DEFAULT_DAMAGE);
            _obstacleSpawner.DestroyObstacle(obstacle);
            if (_failSound != null)
            {
                _soundSource.PlaySound(_failSound);
            }
        }
    }
}
