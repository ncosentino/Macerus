using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Macerus.Plugins.Features.Summoning.Default
{
    public sealed class SummonHandlerFacade : ISummonHandlerFacade
    {
        private readonly Lazy<IReadOnlyCollection<IDiscoverableSummonHandler>> _summonHandlers;

        public SummonHandlerFacade(
            Lazy<IEnumerable<IDiscoverableSummonHandler>> lazySummonHandlers,
            Lazy<ISummonHandlerLoadOrder> lazySummonHandlerLoadOrder)
        {
            _summonHandlers = new Lazy<IReadOnlyCollection<IDiscoverableSummonHandler>>(() =>
                lazySummonHandlers
                    .Value
                    .OrderBy(x => lazySummonHandlerLoadOrder
                        .Value
                        .GetOrder(x))
                    .ToArray());
        }

        public async Task<ISummoningContext> HandleSummoningAsync(ISummoningContext summoningContext)
        {
            var nextContext = summoningContext;
            foreach (var handler in _summonHandlers.Value)
            {
                nextContext = await handler
                    .HandleSummoningAsync(nextContext)
                    .ConfigureAwait(false);
            }

            return nextContext;
        }
    }
}
