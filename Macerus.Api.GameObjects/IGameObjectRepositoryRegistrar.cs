namespace Macerus.Api.GameObjects
{
    public interface IGameObjectRepositoryRegistrar
    {
        void RegisterRepository(
            CanLoadGameObjectDelegate canLoadCallback,
            LoadGameObjectDelegate loadCallback);
    }
}