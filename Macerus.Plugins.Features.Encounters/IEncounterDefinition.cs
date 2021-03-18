using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;

namespace Macerus.Plugins.Features.Encounters
{
    public interface IEncounterDefinition : IHasFilterAttributes
    {
        IIdentifier Id { get; }

        IReadOnlyCollection<IGeneratorComponent> GeneratorComponents { get; }
    }
}
