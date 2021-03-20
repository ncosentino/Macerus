using System;
using System.Collections.Generic;

using Macerus.Api.Behaviors.Filtering;
using Macerus.Plugins.Features.Encounters.SpawnTables.Api;
using Macerus.Plugins.Features.Encounters.SpawnTables.Api.Standard;
using Macerus.Plugins.Features.GameObjects.Actors.Generation;

using NexusLabs.Contracts;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.GameObjects;

namespace Macerus.Plugins.Features.Encounters.SpawnTables.Implementations.Actors
{
    public sealed class ActorSpawnTableHandlerGenerator : IDiscoverableSpawnTableHandlerGenerator
    {
        private readonly IActorGeneratorFacade _actorGeneratorFacade;
        private readonly IFilterContextAmenity _filterContextAmenity;

        public ActorSpawnTableHandlerGenerator(
            IActorGeneratorFacade actorGeneratorFacade,
            IFilterContextAmenity filterContextAmenity)
        {
            _actorGeneratorFacade = actorGeneratorFacade;
            _filterContextAmenity = filterContextAmenity;
        }

        public Type SpawnTableType { get; } = typeof(ActorSpawnTable);

        public IEnumerable<IGameObject> GenerateActors(
            ISpawnTable spawnTable,
            IFilterContext filterContext)
        {
            Contract.Requires(
                spawnTable.GetType() == SpawnTableType,
                $"The provided spawn table '{spawnTable}' must have the type '{SpawnTableType}'.");
            return GenerateActors((IActorSpawnTable)spawnTable, filterContext);
        }

        private IEnumerable<IGameObject> GenerateActors(
            IActorSpawnTable spawnTable,
            IFilterContext filterContext)
        {
            var currentSpawnContext = _filterContextAmenity.CreateSubContext(
                filterContext,
                spawnTable.ProvidedAttributes);
            var generated = _actorGeneratorFacade.GenerateActors(currentSpawnContext);
            return generated;
        }
    }
}