using ProjectXyz.Api.Framework;

namespace Macerus.Api.GameObjects
{
    public interface IDiscoverableGameObjectRepository : IGameObjectRepository
    {
        bool CanLoad(
            IIdentifier typeId,
            IIdentifier objectId);
    }
}