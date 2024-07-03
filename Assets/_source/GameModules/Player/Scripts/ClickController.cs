using Road;
using TinyDI;
using TinyGame;
using TinySounds;
using UnityEngine;

namespace Player
{
    public sealed class ClickController : MonoBehaviour
    {
        private const float DEFAULT_HEAL = 0.1f;

        private PlayerStats _playerStats;

        private ArtefactSpawner _artefactSpawner;

        private GameManager _gameManager;

        [SerializeField]
        private AudioClip _sound;

        private GlobalContext _globalContext;

        private SoundSource _soundSource;

        [Inject]
        private void Construct(PlayerStats playerStats, ArtefactSpawner artefactSpawner, GameManager gameManager, GlobalContext globalContext)
        {
            _playerStats = playerStats;
            _artefactSpawner = artefactSpawner;
            _gameManager = gameManager;
            _globalContext = globalContext;
            _soundSource = _globalContext.Resolve<SoundSource>();
        }

        private void Update()
        {
            if (_gameManager == null)
            {
                return;
            }
            if (!_gameManager.IsGamePlaying)
            {
                return;
            }
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                if (hit.collider != null)
                {
                    Artefact artefact = hit.transform.gameObject.GetComponent<Artefact>();

                    if (artefact == null)
                    {
                        return;
                    }
                    if (_playerStats.horsePower < _playerStats.wagonPower)
                    {
                        _playerStats.IncreaseHorsePower(DEFAULT_HEAL);
                    }
                    else
                    {
                        _playerStats.IncreaseWagonPower(DEFAULT_HEAL);
                    }
                    if (_sound != null)
                    {
                        _soundSource.PlaySound(_sound);
                    }
                    _artefactSpawner.DestroyArtefact(artefact);
                }
            }
        }
    }
}
