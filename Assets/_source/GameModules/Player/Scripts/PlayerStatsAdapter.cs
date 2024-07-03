using System;
using TinyDI;
using TinyGame;

namespace Player
{
    [Serializable]
    public sealed class PlayerStatsAdapter : IGameFinishListener, IGameStartListener
    {
        private PlayerStatsView _playerStatsView;

        private PlayerStats _playerStats;

        [Inject]
        private void Construct(PlayerStatsView playerStatsView, PlayerStats playerStats)
        { 
            _playerStatsView = playerStatsView;
            _playerStats = playerStats;
        }

        public float ExecutionPriority => (int)LoadingPriority.Low;

        public void OnGameFinish()
        {
            _playerStats.OnStatsChange -= _playerStatsView.UpdatePlayerStats;
        }

        public void OnGameStart()
        {
            _playerStats.OnStatsChange += _playerStatsView.UpdatePlayerStats;
        }
    }
}
