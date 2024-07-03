using System.Collections.Generic;

namespace TinyGame
{
    public interface IGameListenerProvider
    {
        IEnumerable<IGameListener> ProvideListeners();
    }
}
