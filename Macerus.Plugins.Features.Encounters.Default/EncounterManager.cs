using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Macerus.Api.Behaviors.Filtering;
using Macerus.Plugins.Features.Encounters.EndHandlers;

using NexusLabs.Contracts;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.Filtering.Default.Attributes;

namespace Macerus.Plugins.Features.Encounters.Default
{
    public sealed class EncounterManager : IEncounterManager
    {
        private readonly IEncounterRepository _encounterRepository;
        private readonly IStartEncounterHandlerFacade _startEncounterHandlerFacade;
        private readonly IEndEncounterHandlerFacade _endEncounterHandlerFacade;
        private readonly IGameObjectFactory _gameObjectFactory;
        private readonly IFilterContextAmenity _filterContextAmenity;
        private readonly IEncounterIdentifiers _encounterIdentifiers;

        private IGameObject _encounter;

        public EncounterManager(
            IEncounterRepository encounterRepository,
            IStartEncounterHandlerFacade startEncounterHandlerFacade,
            IEndEncounterHandlerFacade endEncounterHandlerFacade,
            IGameObjectFactory gameObjectFactory,
            IFilterContextAmenity filterContextAmenity,
            IEncounterIdentifiers encounterIdentifiers)
        {
            _encounterRepository = encounterRepository;
            _startEncounterHandlerFacade = startEncounterHandlerFacade;
            _endEncounterHandlerFacade = endEncounterHandlerFacade;
            _gameObjectFactory = gameObjectFactory;
            _filterContextAmenity = filterContextAmenity;
            _encounterIdentifiers = encounterIdentifiers;
        }

        public event EventHandler<EncounterChangedEventArgs> EncounterChanged;

        public async Task StartEncounterAsync(
            IFilterContext filterContext,
            IIdentifier encounterDefinitioId)
        {
            Contract.Requires(
                _encounter == null,
                $"Cannot start an encounter because '{_encounter}' is already in progress.");
            var encounter = _encounterRepository.GetEncounterById(
                filterContext,
                encounterDefinitioId);

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

        public async Task EndEncounterAsync(
            IFilterContext filterContext,
            IEnumerable<IBehavior> additionalEncounterBehaviors)
        {
            Contract.RequiresNotNull(
                _encounter,
                $"Cannot end an encounter because there is no encounter in progress.");
            var endedEncounter = _gameObjectFactory.Create(_encounter
                .Behaviors
                .Concat(additionalEncounterBehaviors));

            filterContext = _filterContextAmenity
                .ExtendWithSupported(filterContext, new IFilterAttribute[]
                {
                    _filterContextAmenity.CreateSupportedAttribute(
                        _encounterIdentifiers.FilterEncounterDefinitionId,
                        endedEncounter.GetOnly<IReadOnlyIdentifierBehavior>().Id),
                });

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
