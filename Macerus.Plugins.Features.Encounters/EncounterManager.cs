using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Encounters
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

        public void StartEncounter(
            IFilterContext filterContext,
            IIdentifier encounterDefinitioId)
        {
            var encounter = _encounterRepository.GetEncounterById(encounterDefinitioId);
            _startEncounterHandlerFacade.Handle(
                encounter,
                filterContext);
        }
    }
}
