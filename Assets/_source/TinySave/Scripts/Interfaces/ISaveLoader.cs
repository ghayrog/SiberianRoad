using System;
using TinyDI;

namespace TinySave
{
    public interface ISaveLoader
    { 
        void SaveGame(IGameRepository gameRepository, DIContext context);

        void LoadGame(IGameRepository gameRepository, DIContext context);
    }
}
