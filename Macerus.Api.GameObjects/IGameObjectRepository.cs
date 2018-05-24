using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

namespace Macerus.Api.GameObjects
{
    public interface IGameObjectRepository
    {
        IGameObject Load(
            IIdentifier typeId,
            IIdentifier objectId);
    }
}