using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Stats;

namespace Macerus.Plugins.Content.Wip.Stats
{
    public sealed class StatDefinitionToTermMappingRepository : IStatDefinitionToTermMappingRepository
    {
        private static readonly Lazy<IReadOnlyCollection<IStatDefinitionToTermMapping>> _mapping = new Lazy<IReadOnlyCollection<IStatDefinitionToTermMapping>>(() =>
        {
            return StatDefinitions
                .All
                .Select(x => new StatDefinitionToTermMapping(
                    x,
                    Convert(x)))
                .ToArray();
        });

        public IEnumerable<IStatDefinitionToTermMapping> GetStatDefinitionIdToTermMappings() => _mapping.Value;

        public static string Convert(IIdentifier statDefinitionId) =>
            statDefinitionId.ToString().Replace(" ", string.Empty);

        private sealed class StatDefinitionToTermMapping : IStatDefinitionToTermMapping
        {
            public StatDefinitionToTermMapping(IIdentifier stateDefinitionId, string term)
            {
                StatDefinitionId = stateDefinitionId;
                Term = term;
            }

            public IIdentifier StatDefinitionId { get; }

            public string Term { get; }
        }
    }
}
