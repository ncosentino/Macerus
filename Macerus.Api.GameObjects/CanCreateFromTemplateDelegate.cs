using ProjectXyz.Api.Framework;

namespace Macerus.Api.GameObjects
{
    public delegate bool CanCreateFromTemplateDelegate(
        IIdentifier typeId,
        IIdentifier objectId);
}