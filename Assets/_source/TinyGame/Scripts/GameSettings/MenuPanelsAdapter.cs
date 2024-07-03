using System;
using UnityEngine;
using UnityEngine.UI;

namespace TinyGame
{
    [Serializable]
    public sealed class MenuPanelsAdapter
    {
        [SerializeField]
        private Button[] _menuButtons;

        [SerializeField]
        private HideablePanel[] _hideablePanels;

        private int _currentPanel = -1;

        public void Initialize()
        {
            HideAllPanels();

            for (int i = 0; i < _hideablePanels.Length; i++)
            {
                var panelId = i;
                _menuButtons[i].onClick.AddListener(() => { ShowPanel(panelId); });
            }
        }

        private void HideAllPanels()
        {
            for (int i = 0; i < _hideablePanels.Length; i++)
            {
                _hideablePanels[i].Hide();
            }
            _currentPanel = -1;
        }

        private void ShowPanel(int i)
        {
            if (_currentPanel == i)
            {
                HideAllPanels();
                _currentPanel = -1;
                return;
            }
            HideAllPanels();
            _hideablePanels[i].Show();
            _currentPanel = i;
        }

        public void Unsubscribe()
        {
            for (int i = 0; i < _menuButtons.Length; i++)
            {
                _menuButtons[i].onClick.RemoveAllListeners();
            }
        }
    }
}
