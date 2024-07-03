using TinyDI;
using TinyGame;
using UnityEngine;

namespace Map
{
    public sealed class  MapModuleInstaller : MonoBehaviour
    {
        [SerializeField, Service(typeof(MapPointsManager))]
        private MapPointsManager _mapPointsManager;

        [SerializeField, Listener, Service(typeof(MapPointer))]
        private MapPointer _mapPointer;

        [SerializeField,Listener]
        private MapPointerView _mapPointerView;

        [SerializeField, Listener, Service(typeof(CheckPointView))]
        private CheckPointView _checkPointView;

        [SerializeField, Listener, Service(typeof(PauseMenuController))]
        private PauseMenuController _pauseMenuController;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            { 
                _pauseMenuController.OnEscapePressed();
            }
        }
    }
}
