using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;

namespace Macerus.Plugins.Features.Encounters
{
    public sealed class EncounterDefinition : IEncounterDefinition
    {
        public EncounterDefinition(
            IIdentifier id,
            IEnumerable<IFilterAttribute> supportedAttributes,
            IEnumerable<IGeneratorComponent> generatorComponents)
        {
            Id = id;
            SupportedAttributes = supportedAttributes;
            GeneratorComponents = generatorComponents.ToArray();
        }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; }

        public IReadOnlyCollection<IGeneratorComponent> GeneratorComponents { get; }

        public IIdentifier Id { get; }
    }
}
