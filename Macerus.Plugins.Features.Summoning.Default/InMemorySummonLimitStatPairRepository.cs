using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Summoning.Default
{
    public sealed class InMemorySummonLimitStatPairRepository : IDiscoverableSummonLimitStatPairRepository
    {
        private readonly Dictionary<IIdentifier, ISummonLimitStatPair> _statPairs;

        public InMemorySummonLimitStatPairRepository(IEnumerable<ISummonLimitStatPair> statPairs)
        {
            _statPairs = statPairs.ToDictionary(x => x.Id, x => x);
        }

        public async Task<ISummonLimitStatPair> GetPairByIdAsync(IIdentifier id)
        {
            if (!_statPairs.TryGetValue(id, out var pair))
            {
                return null;
            }

            return pair;
        }
    }
}
