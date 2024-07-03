using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TinyGame
{

    public sealed class GameLoader : MonoBehaviour
    {
        private const float PROGRESS_TIMER = 0.25f;

        public event Action<float> OnLoadingProgressUpdate;

        public event Action OnGameLoaded;

        public event Action OnSceneLoading;

        public bool IsGameLoaded { get; private set; }
        public float LoadingProgress { get; private set; }

        [SerializeField]
        private int _mainSceneId = 1;

        private AsyncOperation loadingSceneOperation;

        public bool IsGameContinueAllowed { get; private set; } = false;

        private void Awake()
        {
            IsGameLoaded = false;
            StartCoroutine(LoadGameAsync());
        }

        public void AllowContinue()
        {
            IsGameContinueAllowed = true;
        }

        private IEnumerator LoadGameAsync()
        {
            yield return new WaitForSeconds(PROGRESS_TIMER*4f);
            loadingSceneOperation = SceneManager.LoadSceneAsync(_mainSceneId);
            loadingSceneOperation.allowSceneActivation = false;
            while (loadingSceneOperation.progress < 0.9f)
            {
                yield return new WaitForSeconds(PROGRESS_TIMER);
                loadingSceneOperation.allowSceneActivation = false;
                LoadingProgress = loadingSceneOperation.progress / 0.9f;
                //Debug.Log($"Loading progress: {LoadingProgress}");
                OnLoadingProgressUpdate?.Invoke(LoadingProgress);
            }
            //Debug.Log("Game scene loaded");
            IsGameLoaded = true;
            OnGameLoaded?.Invoke();
        }

        public bool TryLoadGame()
        {
            //Debug.Log("Trying to load...");
            if (IsGameLoaded)
            {
                OnSceneLoading?.Invoke();
                loadingSceneOperation.allowSceneActivation = true;
                return true;
            }
            return false;
        }
    }
}
