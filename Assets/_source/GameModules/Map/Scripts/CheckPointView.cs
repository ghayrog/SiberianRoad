using Player;
using System;
using TinyDI;
using TinyGame;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TinySounds;


namespace Map
{
    [Serializable]
    public sealed class CheckPointView : IGameFinishListener
    {
        [Header("Start Settings")]
        [SerializeField]
        private HideablePanel _beginPanel;

        [SerializeField]
        private Button _beginButton;

        [Header("Check Point Settings")]
        [SerializeField]
        private HideablePanel _checkPointPanel;

        [SerializeField]
        private Button _horseButton;

        [SerializeField]
        private Button _wagonButton;

        [Header("Interest Point Settings")]
        [SerializeField]
        private HideablePanel _interestPointPanel;

        [SerializeField]
        private Text _nameText;

        [SerializeField]
        private Text _descriptionText;

        [SerializeField]
        private Button _continueButton;

        [SerializeField]
        private AudioClip _beginSound;

        [Header("Win Settings")]
        [SerializeField]
        private HideablePanel _winPointPanel;

        public float ExecutionPriority => (int)LoadingPriority.Low;

        private GameManager _gameManager;

        private PlayerStats _playerStats;

        private PauseMenuController _pauseMenuController;

        private GlobalContext _globalContext;

        private SoundSource _soundSource;

        [Inject]
        public void Construct(GameManager gameManager, PlayerStats playerStats, PauseMenuController pauseMenuController, GlobalContext globalContext)
        { 
            _gameManager = gameManager;
            _playerStats = playerStats;
            _pauseMenuController = pauseMenuController;
            _globalContext = globalContext;
            _soundSource = _globalContext.Resolve<SoundSource>();
            _horseButton.onClick.AddListener(OnHorseButtonHandler);
            _wagonButton.onClick.AddListener(OnWagonButtonHandler);
            _continueButton.onClick.AddListener(OnContinueButtonHandler);
            _beginButton.onClick.AddListener(OnBeginButtonHandler);
            _beginPanel.Show();
            if (_beginSound != null) _soundSource.PlaySound(_beginSound);
        }

        public void ShowCheckpointPanel(string title, string description, AudioClip sound, AudioClip music)
        {
            _gameManager.PauseGame();
            _checkPointPanel.Show();
        }

        public void ShowInterestPanel(string title, string description, AudioClip sound, AudioClip music)
        {
            _pauseMenuController.UpdateDiary(description);
            _gameManager.PauseGame();
            _nameText.text = title;
            _descriptionText.text = description;
            _interestPointPanel.Show();
            if (sound != null) _soundSource.PlaySound(sound);
        }

        public void ShowWinPanel()
        {
            _gameManager.PauseGame();
            _winPointPanel.Show();
        }

        private void OnHorseButtonHandler()
        { 
            _playerStats.SetInitialValues(1,_playerStats.wagonPower);
            _gameManager.ResumeGame();
            _checkPointPanel.Hide();
        }

        private void OnWagonButtonHandler()
        {
            _playerStats.SetInitialValues(_playerStats.horsePower, 1);
            _gameManager.ResumeGame();
            _checkPointPanel.Hide();

        }

        private void OnContinueButtonHandler()
        {
            _playerStats.SetInitialValues(1, 1);
            _gameManager.ResumeGame();
            _interestPointPanel.Hide();
        }

        private void OnBeginButtonHandler()
        {
            _gameManager.StartGame();
            _beginPanel.Hide();
        }

        public void OnGameFinish()
        {
            _horseButton.onClick.RemoveListener(OnHorseButtonHandler);
            _wagonButton.onClick.RemoveListener(OnWagonButtonHandler);
            _continueButton.onClick.RemoveListener(OnContinueButtonHandler);
            _beginButton.onClick.RemoveListener(OnBeginButtonHandler);
        }
    }
}
