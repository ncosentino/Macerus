using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Api.GameObjects.Generation;

namespace Macerus.Plugins.Features.GameObjects.Actors.Generation
{
    public sealed class ActorDefinition : IActorDefinition
    {
        public ActorDefinition(
            IEnumerable<IGeneratorComponent> generatorComponents,
            IEnumerable<IFilterAttribute> supportedAttributes)
        {
            GeneratorComponents = generatorComponents.ToArray();
            SupportedAttributes = supportedAttributes;
        }

        public IReadOnlyCollection<IGeneratorComponent> GeneratorComponents { get; }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; }
    }
}
