using System.Collections.Generic;
using ProjectXyz.Plugins.Features.BoundedStats.Api;

namespace Macerus.Plugins.Content.Wip.Stats
{
    public sealed class StatDefinitionIdToBoundsMappingRepository : IStatDefinitionIdToBoundsMappingRepository
    {
        public IEnumerable<IStatDefinitionIdToBoundsMapping> GetStatDefinitionIdToBoundsMappings()
        {
            new StatDefinitionIdToBoundsMapping(StatDefinitions.CurrentLife, new StatBounds("0", StatDefinitionToTermMappingRepository.Convert(StatDefinitions.MaximumLife)));
            new StatDefinitionIdToBoundsMapping(StatDefinitions.CurrentMana, new StatBounds("0", StatDefinitionToTermMappingRepository.Convert(StatDefinitions.MaximumMana)));
            new StatDefinitionIdToBoundsMapping(StatDefinitions.MaximumLife, StatBounds.Min("0"));
            new StatDefinitionIdToBoundsMapping(StatDefinitions.MaximumMana, StatBounds.Min("0"));
            yield break;
        }
    }
}