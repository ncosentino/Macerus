using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;

namespace Macerus.Plugins.Features.GameObjects.Enchantments
{
    public sealed class HasStatGeneratorComponent : IGeneratorComponent
    {
        public HasStatGeneratorComponent(IIdentifier statDefinitionId)
        {
            StatDefinitionId = statDefinitionId;
        }

        public IIdentifier StatDefinitionId { get; set; }
    }
}
