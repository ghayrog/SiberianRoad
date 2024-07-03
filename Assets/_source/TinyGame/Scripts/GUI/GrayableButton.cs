using System;
using UnityEngine;
using UnityEngine.UI;
using TinyInspector;

namespace TinyGame
{
    [Serializable]
    public sealed class GrayableButton
    {
        [field: SerializeField]
        public Button Button { get; private set; }

        [SerializeField]
        private Sprite _grayedOutSprite;

        [SerializeField]
        private Sprite _activeSprite;

        [TinyButton]
        public void GrayOut()
        {
            if (_grayedOutSprite) Button.image.sprite = _grayedOutSprite;
            Button.enabled = false;
        }

        [TinyButton]
        public void ActivateButton()
        {
            if (_grayedOutSprite) Button.image.sprite = _activeSprite;
            Button.enabled = true;
        }
    }
}
