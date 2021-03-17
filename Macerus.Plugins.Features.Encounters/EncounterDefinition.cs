using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.Encounters
{
    public sealed class EncounterDefinition : IHasFilterAttributes
    {
        public EncounterDefinition(
            IEnumerable<IFilterAttribute> supportedAttributes,
            IEnumerable<IGeneratorComponent> generatorComponents)
        {
            SupportedAttributes = supportedAttributes;
            GeneratorComponents = generatorComponents.ToArray();
        }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; }

        public IReadOnlyCollection<IGeneratorComponent> GeneratorComponents { get; }
    }

    public interface IEncounterMapFilterBehavior : IBehavior
    {
        IReadOnlyCollection<IFilterAttribute> ProvidedAttributes { get; }
    }

    public sealed class EncounterMapFilterBehavior :
        BaseBehavior,
        IEncounterMapFilterBehavior
    {
        public IReadOnlyCollection<IFilterAttribute> ProvidedAttributes { get; }
    }

    public sealed class EncounterActorGenerationFilterBehavior :
        BaseBehavior,
        IEncounterMapFilterBehavior
    {
        public IReadOnlyCollection<IFilterAttribute> ProvidedAttributes { get; }
    }
}
