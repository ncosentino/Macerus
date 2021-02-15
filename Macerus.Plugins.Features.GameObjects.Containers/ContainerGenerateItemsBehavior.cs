using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Containers
{
    public sealed class ContainerGenerateItemsBehavior :
        BaseBehavior,
        IReadOnlyContainerGenerateItemsBehavior
    {
        public ContainerGenerateItemsBehavior(IIdentifier dropTableId)
        {
            DropTableId = dropTableId;
        }

        public IIdentifier DropTableId { get; }
    }
}
