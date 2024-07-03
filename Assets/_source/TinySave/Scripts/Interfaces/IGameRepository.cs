namespace TinySave
{
    public interface IGameRepository
    {
        bool LoadState();

        void SaveState();

        void DeleteRepository();

        T GetData<T>();

        bool TryGetData<T>(out T value);

        void SetData<T>(T value);
    }
}
