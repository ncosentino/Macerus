using System.Collections.Generic;
using System.Linq;

using Macerus.Api.Behaviors.Filtering;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Default;

namespace Macerus.Plugins.Features.Spawning.Default
{
    public sealed class ActorSpawnerAmenity : IActorSpawnerAmenity
    {
        private readonly IFilterContextAmenity _filterContextAmenity;
        private readonly IActorSpawner _actorSpawner;
        private readonly ISpawnTableRepositoryFacade _spawnTableRepositoryFacade;
        private readonly ISpawnTableIdentifiers _spawnTableIdentifiers;

        public ActorSpawnerAmenity(
            IFilterContextAmenity filterContextAmenity, 
            IActorSpawner actorSpawner,
            ISpawnTableRepositoryFacade spawnTableRepositoryFacade, 
            ISpawnTableIdentifiers spawnTableIdentifiers)
        {
            _filterContextAmenity = filterContextAmenity;
            _actorSpawner = actorSpawner;
            _spawnTableRepositoryFacade = spawnTableRepositoryFacade;
            _spawnTableIdentifiers = spawnTableIdentifiers;
        }

        public IEnumerable<IGameObject> SpawnActorsFromSpawnTableId(IIdentifier spawnTableId) =>
            SpawnActorsFromSpawnTableId(
                spawnTableId,
                _filterContextAmenity.GetContext());

        public IEnumerable<IGameObject> SpawnActorsFromSpawnTableId(
            IIdentifier spawnTableId,
            IFilterContext filterContext) => SpawnActorsFromSpawnTableId(
                spawnTableId,
                filterContext,
                Enumerable.Empty<IGeneratorComponent>());

        public IEnumerable<IGameObject> SpawnActorsFromSpawnTableId(
            IIdentifier spawnTableId,
            IFilterContext filterContext,
            IEnumerable<IGeneratorComponent> additionalGeneratorComponents)
        {
            var spawnTable = _spawnTableRepositoryFacade.GetForSpawnTableId(spawnTableId);
            var spawnTableAttribute = _filterContextAmenity.CreateRequiredAttribute(
                _spawnTableIdentifiers.FilterContextSpawnTableIdentifier,
                spawnTableId);
            var spawnFilterContext = filterContext
                .WithAdditionalAttributes(new[] { spawnTableAttribute })
                .WithRange(
                    spawnTable.MinimumGenerateCount,
                    spawnTable.MaximumGenerateCount);

            return _actorSpawner.SpawnActors(
                spawnFilterContext,
                additionalGeneratorComponents);
        }
    }
}