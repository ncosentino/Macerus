using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Api.Behaviors.Filtering;
using Macerus.Plugins.Features.Encounters.SpawnTables.Api;
using Macerus.Plugins.Features.Encounters.SpawnTables.Api.Standard;
using Macerus.Plugins.Features.GameObjects.Actors.Generation;

using NexusLabs.Contracts;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.Filtering.Api;

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
            IFilterContext filterContext,
            IEnumerable<IGeneratorComponent> additionalActorGeneratorComponents)
        {
            Contract.Requires(
                spawnTable.GetType() == SpawnTableType,
                $"The provided spawn table '{spawnTable}' must have the type '{SpawnTableType}'.");
            return GenerateActors(
                (IActorSpawnTable)spawnTable,
                filterContext,
                additionalActorGeneratorComponents);
        }

        private IEnumerable<IGameObject> GenerateActors(
            IActorSpawnTable spawnTable,
            IFilterContext filterContext,
            IEnumerable<IGeneratorComponent> additionalActorGeneratorComponents)
        {
            var currentSpawnContext = _filterContextAmenity.CreateSubContext(
                filterContext,
                spawnTable.ProvidedAttributes);
            var generatorComponents = spawnTable
                .ProvidedGeneratorComponents
                .Concat(additionalActorGeneratorComponents);
            var generated = _actorGeneratorFacade.GenerateActors(
                currentSpawnContext,
                generatorComponents);
            return generated;
        }
    }
}