using System;
using TinyDI;
using TinyGame;
using UnityEngine;
using UnityEngine.UI;

namespace Map
{
    [Serializable]
    public sealed class MapPointerView : IGameFinishListener
    {
        [SerializeField]
        private Button[] _resetButtons;

        [SerializeField]
        private HideablePanel[] _panels;

        private MapPointer _mapPointer;

        public float ExecutionPriority => (int)LoadingPriority.Low;

        public void OnGameFinish()
        {
            foreach (var button in _resetButtons)
            {
                button.onClick.RemoveListener(OnResetHandler);
            }
        }

        [Inject]
        private void Construct(MapPointer mapPointer)
        { 
            _mapPointer = mapPointer;
            foreach (var button in _resetButtons)
            {
                button.onClick.AddListener(OnResetHandler);
            }
        }

        private void OnResetHandler()
        { 
            _mapPointer.ResetPointer();
            foreach (var panel in _panels)
            {
                panel.Hide();
            }
        }
    }
}
