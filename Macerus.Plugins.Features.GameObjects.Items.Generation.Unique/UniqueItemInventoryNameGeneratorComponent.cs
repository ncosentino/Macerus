using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;

namespace Macerus.Plugins.Features.GameObjects.Items
{
    public sealed class UniqueItemInventoryNameGeneratorComponent
        : IGeneratorComponent
    {
        public UniqueItemInventoryNameGeneratorComponent(IIdentifier stringResourceId)
        {
            StringResourceId = stringResourceId;
        }

        public IIdentifier StringResourceId { get; }
    }
}
