using Player;
using System;
using TinyDI;
using TinyGame;
using UnityEngine;
using UnityEngine.UI;

namespace Map
{
    [Serializable]
    public sealed class PauseMenuController
    {
        public float ExecutionPriority => (int)LoadingPriority.Low;

        [SerializeField]
        private HideablePanel _pausePanel;

        [SerializeField]
        private Text _diaryText;

        private GameManager _gameManager;

        private PlayerStats _playerStats;

        private bool _isShowing = false;

        [Inject]
        public void Construct(GameManager gameManager, PlayerStats playerStats)
        {
            _gameManager = gameManager;
            _playerStats = playerStats;
        }

        public void OnEscapePressed()
        {
            if (_gameManager.IsGamePlaying)
            {
                _gameManager.PauseGame();
                _pausePanel.Show();
                _isShowing = true;
            }
            else if (_gameManager.IsGamePaused && _isShowing)
            {
                _pausePanel.Hide();
                _gameManager.ResumeGame();
                _isShowing = false;
            }

        }

        public void UpdateDiary(string diaryText)
        {
            _diaryText.text = diaryText;
        }
    }
}
