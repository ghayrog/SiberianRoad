using TinyInspector;
using UnityEngine;

namespace TinyGame
{
    public sealed class HideablePanel : MonoBehaviour
    {
        [TinyPlayButton]
        public void Show()
        {
            gameObject.SetActive(true);
        }

        [TinyPlayButton]
        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
