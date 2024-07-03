using System;

namespace Player
{
    [Serializable]
    public struct PlayerStatsState
    {
        public float horsePower;
        public float wagonPower;
        public int lastCheckpointId;
    }
}
