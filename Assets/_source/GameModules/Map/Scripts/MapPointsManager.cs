using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    [Serializable]
    public class MapPointsManager
    {
        [SerializeField]
        private Transform[] _mapPoints;

        public Transform GetPoint(int i)
        {
            if (i >= _mapPoints.Length)
            {
                return null;
            }
            return _mapPoints[i];
        }
    }
}
