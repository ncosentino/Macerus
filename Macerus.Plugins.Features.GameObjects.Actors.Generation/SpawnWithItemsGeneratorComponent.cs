using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;

namespace Macerus.Plugins.Features.GameObjects.Actors.Generation
{
    public sealed class SpawnWithItemsGeneratorComponent : IGeneratorComponent
    {
        public SpawnWithItemsGeneratorComponent(IIdentifier dropTableId)
        {
            DropTableId = dropTableId;
        }

        public IIdentifier DropTableId { get; }
    }
}
