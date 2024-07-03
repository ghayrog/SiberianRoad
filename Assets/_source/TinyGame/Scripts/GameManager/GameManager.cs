using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyInspector;
using UnityEngine.SceneManagement;

namespace TinyGame
{
    public sealed class GameManager : MonoBehaviour
    {
        [ReadOnlyInspector, SerializeField]
        private GameState _gameState = GameState.None;

        public bool IsGamePaused => (_gameState == GameState.Paused);
        public bool IsGamePlaying => (_gameState == GameState.Playing);

        private readonly List<IGameStartListener> gameStartListeners = new();
        private readonly List<IGameFinishListener> gameFinishListeners = new();
        private readonly List<IGamePauseListener> gamePauseListeners = new();
        private readonly List<IGameResumeListener> gameResumeListeners = new();

        private readonly List<IGameUpdateListener> gameUpdateListeners = new();
        private readonly List<IGameLateUpdateListener> gameLateUpdateListeners = new();
        private readonly List<IGameFixedUpdateListener> gameFixedUpdateListeners = new();

        private void FixedUpdate()
        {
            if (_gameState != GameState.Playing) return;
            for (var i = 0; i < gameFixedUpdateListeners.Count; i++)
            {
                gameFixedUpdateListeners[i].OnFixedUpdate(Time.fixedDeltaTime);
            }
        }

        private void Update()
        {
            if (_gameState != GameState.Playing) return;
            for (var i = 0; i < gameUpdateListeners.Count; i++)
            {
                gameUpdateListeners[i].OnUpdate(Time.deltaTime);
            }
        }

        private void LateUpdate()
        {
            if (_gameState != GameState.Playing) return;
            for (var i = 0; i < gameLateUpdateListeners.Count; i++)
            {
                gameLateUpdateListeners[i].OnLateUpdate(Time.deltaTime);
            }
        }

        internal void AddMultipleListeners(IEnumerable<IGameListener> listeners)
        {
            foreach (var listener in listeners)
            {
                AddListener(listener);
            }
        }

        internal void AddListener(IGameListener listener)
        {
            if (listener is IGameStartListener startListener)
            {
                gameStartListeners.Add(startListener);
                gameStartListeners.Sort(
                    (IGameStartListener listener1, IGameStartListener listener2) =>
                    {
                        return (int)Mathf.Sign(listener1.ExecutionPriority - listener2.ExecutionPriority);
                    }
                    );
            }
            if (listener is IGameFinishListener finishListener)
            {
                gameFinishListeners.Add(finishListener);
            }
            if (listener is IGamePauseListener pauseListener)
            {
                gamePauseListeners.Add(pauseListener);
            }
            if (listener is IGameResumeListener resumeListener)
            {
                gameResumeListeners.Add(resumeListener);
            }

            if (listener is IGameUpdateListener updateListener)
            {
                gameUpdateListeners.Add(updateListener);
            }
            if (listener is IGameFixedUpdateListener fixedUpdateListener)
            {
                gameFixedUpdateListeners.Add(fixedUpdateListener);
            }
            if (listener is IGameLateUpdateListener lateUpdateListener)
            {
                gameLateUpdateListeners.Add(lateUpdateListener);
            }
        }

        [TinyPlayButton]
        public void StartGame()
        {
            if (_gameState != GameState.None && _gameState != GameState.Finished) return;
            for (var i = 0; i < gameStartListeners.Count; i++)
            {
                gameStartListeners[i].OnGameStart();
            }
            _gameState = GameState.Playing;
            Debug.Log("Start");
        }

        [TinyPlayButton]
        public void PauseGame()
        {
            if (_gameState != GameState.Playing) return;
            for (var i = 0; i < gamePauseListeners.Count; i++)
            {
                gamePauseListeners[i].OnGamePause();
            }
            _gameState = GameState.Paused;
            Debug.Log("Pause");
        }

        [TinyPlayButton]
        public void ResumeGame()
        {
            if (_gameState != GameState.Paused) return;
            for (var i = 0; i < gameResumeListeners.Count; i++)
            {
                gameResumeListeners[i].OnGameResume();
            }
            _gameState = GameState.Playing;
            Debug.Log("Resume");
        }

        [TinyPlayButton]
        public void FinishGame()
        {
            if (_gameState == GameState.None || _gameState == GameState.Finished) return;
            for (var i = 0; i < gameFinishListeners.Count; i++)
            {
                gameFinishListeners[i].OnGameFinish();
            }
            _gameState = GameState.Finished;
            Debug.Log("Game over!");
        }

        
        private IEnumerator ResetGame()
        {
            yield return new WaitForFixedUpdate();
            SceneManager.LoadScene(0);
        }

        [TinyPlayButton]
        public void BackToFirstScene()
        {
            FinishGame();
            if (GlobalContext.Instance != null) Destroy(GlobalContext.Instance.gameObject);
            GlobalContext.Instance = null;
            StartCoroutine(ResetGame());
        }
    }
}
