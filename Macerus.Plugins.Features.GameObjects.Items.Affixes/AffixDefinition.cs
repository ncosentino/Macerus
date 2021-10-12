using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.GameObjects.Items.Affixes.Api;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace Macerus.Plugins.Features.GameObjects.Items.Affixes.Default
{
    public sealed class AffixDefinition : IAffixDefinition
    {
        public AffixDefinition(
            IIdentifier id,
            IEnumerable<IGeneratorComponent> generatorComponents, 
            IEnumerable<IFilterAttribute> supportedAttributes)
        {
            Id = id;
            GeneratorComponents = generatorComponents.ToArray();
            SupportedAttributes = supportedAttributes;
        }

        public IIdentifier Id { get; }

        public IReadOnlyCollection<IGeneratorComponent> GeneratorComponents { get; }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; }
    }
}
