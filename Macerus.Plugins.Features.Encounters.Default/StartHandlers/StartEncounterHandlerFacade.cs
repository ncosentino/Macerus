using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace Macerus.Plugins.Features.Encounters.Default.StartHandlers
{
    public sealed class StartEncounterHandlerFacade : IStartEncounterHandlerFacade
    {
        private readonly Lazy<IReadOnlyCollection<IDiscoverableStartEncounterHandler>> _lazyStartEncounterHandlers;

        public StartEncounterHandlerFacade(
            Lazy<IEnumerable<IDiscoverableStartEncounterHandler>> lazyStartEncounterHandlers,
            IEncounterStartLoaderOrder encounterStartLoaderOrder)
        {
            _lazyStartEncounterHandlers = new Lazy<IReadOnlyCollection<IDiscoverableStartEncounterHandler>>(() =>
                lazyStartEncounterHandlers
                    .Value
                    .OrderBy(x => encounterStartLoaderOrder.GetOrder(x))
                    .ToArray());
        }

        public async Task HandleAsync(
            IGameObject encounter,
            IFilterContext filterContext)
        {
            foreach (var handler in _lazyStartEncounterHandlers.Value)
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
