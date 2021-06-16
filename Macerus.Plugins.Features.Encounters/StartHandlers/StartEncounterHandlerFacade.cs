using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace Macerus.Plugins.Features.Encounters
{
    public sealed class StartEncounterHandlerFacade : IStartEncounterHandlerFacade
    {
        private readonly IReadOnlyCollection<IDiscoverableStartEncounterHandler> _startEncounterHandlers;

        public StartEncounterHandlerFacade(IEnumerable<IDiscoverableStartEncounterHandler> startEncounterHandlers)
        {
            _startEncounterHandlers = startEncounterHandlers.ToArray();
        }

        public async Task HandleAsync(
            IGameObject encounter,
            IFilterContext filterContext)
        {
            foreach (var handler in _startEncounterHandlers.OrderBy(x => x.Priority))
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
