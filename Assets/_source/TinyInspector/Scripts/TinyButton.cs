using UnityEngine;

namespace TinyInspector
{
    public class TinyButton : MonoBehaviour
    {
        [field: SerializeField]
        public MonoBehaviour TargetMonoBehaviour { get; private set; }
    }
}
