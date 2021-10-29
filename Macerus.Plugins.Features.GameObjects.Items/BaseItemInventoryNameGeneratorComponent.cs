using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;

namespace Macerus.Plugins.Features.GameObjects.Items
{
    public sealed class BaseItemInventoryNameGeneratorComponent
        : IGeneratorComponent
    {
        public BaseItemInventoryNameGeneratorComponent(IIdentifier stringResourceId)
        {
            StringResourceId = stringResourceId;
        }

        public IIdentifier StringResourceId { get; }
    }
}
