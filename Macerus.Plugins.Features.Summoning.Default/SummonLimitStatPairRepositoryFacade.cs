using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Summoning.Default
{
    public sealed class SummonLimitStatPairRepositoryFacade : ISummonLimitStatPairRepositoryFacade
    {
        private readonly IReadOnlyCollection<IDiscoverableSummonLimitStatPairRepository> _repositories;

        public SummonLimitStatPairRepositoryFacade(IEnumerable<IDiscoverableSummonLimitStatPairRepository> repositories)
        {
            _repositories = repositories.ToArray();
        }

        public async Task<ISummonLimitStatPair> GetPairByIdAsync(IIdentifier id)
        {
            var tasks = _repositories.Select(x => x.GetPairByIdAsync(id));
            var results = await Task.WhenAll(tasks);

            var result = results.FirstOrDefault(x => x != null);
            if (result == null)
            {
                throw new KeyNotFoundException(
                    $"Could not find a pair with ID '{id}'.");
            }

            return result;
        }
    }
}
