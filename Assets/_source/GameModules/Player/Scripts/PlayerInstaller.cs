using TinyDI;
using TinyGame;
using TinyInspector;
using UnityEngine;

namespace Player
{
    public class PlayerInstaller : MonoBehaviour
    {
        [SerializeField, Listener, Service(typeof(PlayerController))]
        private PlayerController _playerController;

        [Listener, Service(typeof(PlayerInput))]
        private PlayerInput _playerInput = new();

        [SerializeField, Listener, Service(typeof(PlayerStats))]
        private PlayerStats _playerStats;

        [SerializeField, Listener,Service(typeof(PlayerStatsView))]
        private PlayerStatsView _playerStatsView;

        [Listener]
        private PlayerStatsAdapter _playerStatsAdapter = new();

        [SerializeField,Listener]
        private GameOverManager _gameOverManager;

        [SerializeField]
        private WagonController _wagonController;

        [SerializeField]
        private HorseController _horseController;

        [SerializeField]
        private ClickController _clickController;

        [TinyPlayButton]
        private void IncreasePosition()
        {
            _playerController.IncreasePosition();
        }

        [TinyPlayButton]
        private void DecreasePosition()
        {
            _playerController.DecreasePosition();
        }

        [TinyPlayButton]
        private void IncreaseHorsePower()
        {
            _playerStats.IncreaseHorsePower(0.25f);
        }

        [TinyPlayButton]
        private void DecreaseHorsePower()
        {
            _playerStats.DecreaseHorsePower(0.25f);
        }

        [TinyPlayButton]
        private void IncreaseWagonPower()
        {
            _playerStats.IncreaseWagonPower(0.25f);
        }

        [TinyPlayButton]
        private void DecreaseWagonPower()
        {
            _playerStats.DecreaseWagonPower(0.25f);
        }

        private void OnEnable()
        {
        }

        private void OnDisable()
        {
            _gameOverManager.Unsubscribe();
        }
    }

}
