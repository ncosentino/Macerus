using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Containers
{
    public sealed class ContainerGenerateItemsBehavior :
        BaseBehavior,
        IContainerGenerateItemsBehavior
    {
        public ContainerGenerateItemsBehavior(IIdentifier dropTableId, bool hasGeneratedItems)
        {
            DropTableId = dropTableId;
            HasGeneratedItems = hasGeneratedItems;
        }

        public IIdentifier DropTableId { get; set; }

        public bool HasGeneratedItems { get; set; }
    }
}
