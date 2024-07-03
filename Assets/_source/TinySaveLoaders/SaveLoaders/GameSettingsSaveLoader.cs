using TinyGame;
using TinySave;

namespace TinySaveLoaders
{
    internal sealed class GameSettingsSaveLoader : SaveLoader<GameSettingsState, GameSettings>
    {
        protected override GameSettingsState ConvertToData(GameSettings service)
        {
            GameSettingsState state = new GameSettingsState
            { 
                SoundVolume = service.SoundVolume,
                MusicVolume = service.MusicVolume,
            };

            return state;
        }

        protected override void SetupData(GameSettings service, GameSettingsState data)
        {
            service.SetVolumes(data.SoundVolume, data.MusicVolume);
        }
    }
}
