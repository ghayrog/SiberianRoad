using TinyDI;
using UnityEngine;

namespace UnitPositions
{
    internal class StaticUnitsModuleInstaller : MonoBehaviour
    {
        [Service(typeof(StaticUnitPositionsCollector))]
        private StaticUnitPositionsCollector collector = new();

        [Inject]
        private void Construct()
        { 
            collector.CollectPositions();
        }
    }
}
