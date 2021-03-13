using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.GameObjects.Generation;

namespace Macerus.Plugins.Features.GameObjects.Actors.Generation
{
    public interface IActorDefinition : IHasFilterAttributes
    {
        IReadOnlyCollection<IGeneratorComponent> GeneratorComponents { get; }
    }
}
