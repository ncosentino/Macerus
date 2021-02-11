using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

namespace Macerus.Api.GameObjects
{
    public delegate IGameObject LoadGameObjectDelegate(
        IIdentifier typeId,
        IIdentifier objectId);
}