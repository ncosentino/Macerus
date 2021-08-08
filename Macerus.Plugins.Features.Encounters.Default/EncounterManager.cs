using System;
using System.Threading.Tasks;

using Macerus.Plugins.Features.Encounters.EndHandlers;

using NexusLabs.Contracts;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace Macerus.Plugins.Features.Encounters.Default
{
    public sealed class EncounterManager : IEncounterManager
    {
        private readonly IEncounterRepository _encounterRepository;
        private readonly IStartEncounterHandlerFacade _startEncounterHandlerFacade;
        private readonly IEndEncounterHandlerFacade _endEncounterHandlerFacade;

        private IGameObject _encounter;

        public EncounterManager(
            IEncounterRepository encounterRepository,
            IStartEncounterHandlerFacade startEncounterHandlerFacade,
            IEndEncounterHandlerFacade endEncounterHandlerFacade)
        {
            _encounterRepository = encounterRepository;
            _startEncounterHandlerFacade = startEncounterHandlerFacade;
            _endEncounterHandlerFacade = endEncounterHandlerFacade;
        }

        public event EventHandler<EncounterChangedEventArgs> EncounterChanged;

        public async Task StartEncounterAsync(
            IFilterContext filterContext,
            IIdentifier encounterDefinitioId)
        {
            Contract.Requires(
                _encounter == null,
                $"Cannot start an encounter because '{_encounter}' is already in progress.");
            var encounter = _encounterRepository.GetEncounterById(encounterDefinitioId);

            await _startEncounterHandlerFacade
                .HandleAsync(
                    encounter,
                    filterContext)
                .ConfigureAwait(false);
            await EncounterChanged
                .InvokeOrderedAsync(this, new EncounterChangedEventArgs(
                    encounter,
                    null))
                .ConfigureAwait(false);
            _encounter = encounter;
        }

        public async Task EndEncounterAsync(IFilterContext filterContext)
        {
            Contract.RequiresNotNull(
                _encounter,
                $"Cannot end an encounter because there is no encounter in progress.");
            var endedEncounter = _encounter;

            await _endEncounterHandlerFacade
                .HandleAsync(
                    endedEncounter,
                    filterContext)
                .ConfigureAwait(false);
            await EncounterChanged
                .InvokeOrderedAsync(this, new EncounterChangedEventArgs(
                    endedEncounter,
                    null))
                .ConfigureAwait(false);
            _encounter = null;
        }
    }
}
