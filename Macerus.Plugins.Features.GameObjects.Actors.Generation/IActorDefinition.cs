using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Api.GameObjects.Generation;

namespace Macerus.Plugins.Features.GameObjects.Actors.Generation
{
    public interface IActorDefinition : IHasFilterAttributes
    {
        IReadOnlyCollection<IGeneratorComponent> GeneratorComponents { get; }
    }
}
