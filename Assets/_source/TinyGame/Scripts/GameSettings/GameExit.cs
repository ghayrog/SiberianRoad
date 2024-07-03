using System;
using UnityEngine;
using UnityEngine.UI;

namespace TinyGame
{
    [Serializable]
    public sealed class GameExit
    {
        [SerializeField]
        private Button _exitButton;

        public void Initialize()
        {
            _exitButton.onClick.AddListener(ExitGame);
        }

        private void ExitGame()
        {
            Application.Quit();
        }

        public void Unsubscribe()
        {
            _exitButton.onClick.RemoveListener(ExitGame);
        }
    }
}
