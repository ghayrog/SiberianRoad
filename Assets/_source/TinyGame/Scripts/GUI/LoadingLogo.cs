using TinyInspector;
using UnityEngine;

namespace TinyGame
{
    [RequireComponent(typeof(Animator))]
    public sealed class LoadingLogo : MonoBehaviour
    {
        private const string IS_LOADING_ANIMATOR_NAME = "IsLoading";
        private Animator _animator;


        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        [TinyPlayButton]
        public void Show()
        {
            if (_animator == null) _animator = GetComponent<Animator>();
            _animator.SetBool(IS_LOADING_ANIMATOR_NAME, true);
        }

        [TinyPlayButton]
        public void Hide()
        {
            _animator?.SetBool(IS_LOADING_ANIMATOR_NAME, false);
        }
    }
}
