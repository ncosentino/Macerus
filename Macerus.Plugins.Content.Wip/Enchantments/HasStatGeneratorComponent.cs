using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Api.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Macerus.Plugins.Content.Wip.Enchantments
{
    public sealed class HasStatGeneratorComponent : IGeneratorComponent
    {
        public HasStatGeneratorComponent(IIdentifier statDefinitionId)
        {
            StatDefinitionId = statDefinitionId;
        }

        public IEnumerable<IGeneratorAttribute> SupportedAttributes { get; } = Enumerable.Empty<IGeneratorAttribute>();

        public IIdentifier StatDefinitionId { get; }
    }
}
