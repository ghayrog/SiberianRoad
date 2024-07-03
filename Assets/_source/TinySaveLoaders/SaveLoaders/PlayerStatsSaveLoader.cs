using Player;
using TinySave;

namespace TinySaveLoaders
{
    public sealed class PlayerStatsSaveLoader : SaveLoader<PlayerStatsState, PlayerStats>
    {
        protected override PlayerStatsState ConvertToData(PlayerStats service)
        {
            return new PlayerStatsState
            {
                horsePower = service.horsePower,
                wagonPower = service.wagonPower,
                lastCheckpointId = service.lastCheckpointId,
            };
        }

        protected override void SetupData(PlayerStats service, PlayerStatsState data)
        {
            service.lastCheckpointId = data.lastCheckpointId;
            service.SetInitialValues(data.horsePower, data.wagonPower);
        }
    }
}
