using System;
using System.Collections.Generic;

namespace UnitPositions
{
    [Serializable]
    public struct UnitPositionsState
    {
        public Dictionary<string, TransformStruct> transforms;
    }
}
