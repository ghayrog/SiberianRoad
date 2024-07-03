using System;
using TinyDI;

namespace TinySave
{
    public abstract class SaveLoader<TData, TService> : ISaveLoader where TService : class
    {
        void ISaveLoader.LoadGame(IGameRepository repository, DIContext context)
        {
            var service = context.Resolve<TService>();
            if (repository.TryGetData(out TData data))
            {
                this.SetupData(service, data);
            }
            else
            {
                this.SetupByDefault(service);
            }
        }

        void ISaveLoader.SaveGame(IGameRepository repository, DIContext context)
        {
            var service = context.Resolve<TService>();
            var data = this.ConvertToData(service);
            repository.SetData(data);
        }

        protected abstract void SetupData(TService service, TData data);

        protected abstract TData ConvertToData(TService service);

        protected virtual void SetupByDefault(TService service)
        {
        }
    }
}
