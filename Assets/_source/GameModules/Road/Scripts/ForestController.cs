using TinyGame;
using UnityEngine;

namespace Road
{
    public class ForestController : MonoBehaviour, IGameUpdateListener
    {
        [SerializeField]
        private float _speed;

        [SerializeField]
        private Transform _forest1;

        [SerializeField]
        private Transform _forest2;

        [SerializeField]
        private float _xLeftPosition = -70.0558f;

        [SerializeField]
        private float _xRightPosition = 72.31f;

        public float ExecutionPriority => (int)LoadingPriority.Low;

        public void OnUpdate(float deltaTime)
        {
            _forest1.Translate(new Vector3(-_speed * deltaTime, 0, 0));
            _forest2.Translate(new Vector3(-_speed * deltaTime, 0, 0));
            if (_forest1.position.x < _xLeftPosition)
            {
                _forest1.position = new Vector3(_xRightPosition, _forest1.position.y, _forest1.position.z);
            }

            if (_forest2.position.x < _xLeftPosition)
            {
                _forest2.position = new Vector3(_xRightPosition, _forest2.position.y, _forest2.position.z);
            }
        }
    }
}
