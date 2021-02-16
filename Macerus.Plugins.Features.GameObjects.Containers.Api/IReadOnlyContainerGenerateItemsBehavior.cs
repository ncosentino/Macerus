using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.GameObjects.Containers
{
    public interface IReadOnlyContainerGenerateItemsBehavior : IBehavior
    {
        IIdentifier DropTableId { get; }

        bool HasGeneratedItems { get; }
    }
}