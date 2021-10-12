using System.Collections.Generic;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace Macerus.Plugins.Features.GameObjects.Items.Affixes.Api
{
    public interface IAffixDefinition : IHasFilterAttributes
    {
        IIdentifier Id { get; }

        IReadOnlyCollection<IGeneratorComponent> GeneratorComponents { get; }
    }
}