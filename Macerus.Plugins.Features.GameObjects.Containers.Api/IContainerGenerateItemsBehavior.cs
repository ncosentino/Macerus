using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.GameObjects.Containers
{
    public interface IContainerGenerateItemsBehavior : IReadOnlyContainerGenerateItemsBehavior
    {
        new IIdentifier DropTableId { get; set; }

        new bool HasGeneratedItems { get; set; }
    }
}