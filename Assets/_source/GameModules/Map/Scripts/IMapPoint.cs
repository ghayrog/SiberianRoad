using Player;
using UnityEngine;

namespace Map
{
    public interface IMapPoint
    {
        void ActivatePoint(PlayerStats playerStats, int mapPointId, CheckPointView checkPointView);
    }
}
