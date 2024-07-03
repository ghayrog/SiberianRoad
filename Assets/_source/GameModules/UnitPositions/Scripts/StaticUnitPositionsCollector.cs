using System.Collections.Generic;
using UnityEngine;

namespace UnitPositions
{

    public class StaticUnitPositionsCollector
    {
        public readonly Dictionary<string, Transform> transforms = new();

        public void CollectPositions()
        {
            transforms.Clear();
            SaveableStaticUnit[] saveableStaticUnits = GameObject.FindObjectsOfType<SaveableStaticUnit>();
            foreach (var unit in saveableStaticUnits)
            {
                transforms.Add(unit.keyString, unit.gameObject.transform);
            }
            Debug.Log($"Static Units found: {saveableStaticUnits.Length}");
        }
    }
}
