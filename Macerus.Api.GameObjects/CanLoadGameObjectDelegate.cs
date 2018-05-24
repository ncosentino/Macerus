using ProjectXyz.Api.Framework;

namespace Macerus.Api.GameObjects
{
    public delegate bool CanLoadGameObjectDelegate(
        IIdentifier typeId,
        IIdentifier objectId);
}