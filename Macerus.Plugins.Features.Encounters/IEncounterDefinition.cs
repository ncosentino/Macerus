using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
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
