using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Macerus.Plugins.Features.Encounters.EndHandlers;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace Macerus.Plugins.Features.Encounters.Default.EndHandlers
{
    public sealed class EndEncounterHandlerFacade : IEndEncounterHandlerFacade
    {
        private readonly Lazy<IReadOnlyCollection<IDiscoverableEndEncounterHandler>> _lazyEndEncounterHandlers;

        public EndEncounterHandlerFacade(
            Lazy<IEnumerable<IDiscoverableEndEncounterHandler>> lazyEndEncounterHandlers,
            IEncounterEndLoaderOrder encounterEndLoaderOrder)
        {
            _lazyEndEncounterHandlers = new Lazy<IReadOnlyCollection<IDiscoverableEndEncounterHandler>>(() =>
                lazyEndEncounterHandlers
                    .Value
                    .OrderBy(x => encounterEndLoaderOrder.GetOrder(x))
                    .ToArray());
        }

        public async Task HandleAsync(
            IGameObject encounter,
            IFilterContext filterContext)
        {
            foreach (var handler in _lazyEndEncounterHandlers.Value)
            {
                await handler
                    .HandleAsync(
                        encounter,
                        filterContext)
                    .ConfigureAwait(false);
            }
        }
    }
}
