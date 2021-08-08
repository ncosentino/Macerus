using System.Threading.Tasks;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace Macerus.Plugins.Features.Encounters.Default
{
    public sealed class EncounterManager : IEncounterManager
    {
        private readonly IEncounterRepository _encounterRepository;
        private readonly IStartEncounterHandlerFacade _startEncounterHandlerFacade;

        public EncounterManager(
            IEncounterRepository encounterRepository,
            IStartEncounterHandlerFacade startEncounterHandlerFacade)
        {
            _encounterRepository = encounterRepository;
            _startEncounterHandlerFacade = startEncounterHandlerFacade;
        }

        public async Task StartEncounterAsync(
            IFilterContext filterContext,
            IIdentifier encounterDefinitioId)
        {
            var encounter = _encounterRepository.GetEncounterById(encounterDefinitioId);
            await _startEncounterHandlerFacade
                .HandleAsync(
                    encounter,
                    filterContext)
                .ConfigureAwait(false);
        }
    }
}
