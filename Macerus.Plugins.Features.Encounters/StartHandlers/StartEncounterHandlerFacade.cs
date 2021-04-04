using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.Encounters
{
    public sealed class StartEncounterHandlerFacade : IStartEncounterHandlerFacade
    {
        private readonly IReadOnlyCollection<IDiscoverableStartEncounterHandler> _startEncounterHandlers;

        public StartEncounterHandlerFacade(IEnumerable<IDiscoverableStartEncounterHandler> startEncounterHandlers)
        {
            _startEncounterHandlers = startEncounterHandlers.ToArray();
        }

        public void Handle(
            IGameObject encounter,
            IFilterContext filterContext) => _startEncounterHandlers
            .OrderBy(x => x.Priority)
            .Foreach(x => x.Handle(
                encounter,
                filterContext));
    }
}
