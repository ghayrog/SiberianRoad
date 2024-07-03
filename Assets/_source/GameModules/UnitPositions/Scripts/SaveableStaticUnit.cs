using UnityEngine;

namespace UnitPositions
{
    public sealed class SaveableStaticUnit : MonoBehaviour
    {
        [field:SerializeField]
        public string keyString { get; private set; }
    }
}
