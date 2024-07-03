using System;
using TinyDI;
using TinyGame;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    [Serializable]
    public class GameOverManager
    {
        [SerializeField]
        private HideablePanel _gameOverPanel;

        [SerializeField]
        private Button[] _exitGameButtons;

        private PlayerStats _playerStats;

        private GameManager _gameManager;

        [Inject]
        private void Construct(PlayerStats playerStats, GameManager gameManager)
        { 
            _playerStats = playerStats;
            _gameManager = gameManager;
            _gameOverPanel.Hide();
            _playerStats.OnLowHorsePower += ShowGameOverPanel;
            _playerStats.OnLowWagonPower += ShowGameOverPanel;
            foreach (var button in _exitGameButtons)
            {
                button.onClick.AddListener(ExitToMainMenu);
            }
        }

        private void ShowGameOverPanel()
        {
            _gameManager.PauseGame();
            _gameOverPanel.Show();
        }

        public void Unsubscribe()
        {
            _playerStats.OnLowHorsePower -= ShowGameOverPanel;
            _playerStats.OnLowWagonPower -= ShowGameOverPanel;
            foreach (var button in _exitGameButtons)
            {
                button.onClick.RemoveListener(ExitToMainMenu);
            }
        }

        public void ExitToMainMenu()
        {
            _gameManager.BackToFirstScene();
        }
    }
}
