using UnityEngine;
using UnityEngine.SceneManagement;

namespace TinyGame
{
    public sealed class GlobalGameProperties : MonoBehaviour
    {
        [field:SerializeField]
        public bool IsGameContinued { get; set; }  = false;
    }
}
