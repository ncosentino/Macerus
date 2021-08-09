using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using Macerus.Api.Behaviors.Filtering;
using Macerus.Plugins.Features.Encounters;
using Macerus.Plugins.Features.Encounters.Default;
using Macerus.Plugins.Features.GameObjects.Actors.Generation;
using Macerus.Plugins.Features.Mapping.Default;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Game.Api;
using ProjectXyz.Game.Core;
using ProjectXyz.Game.Interface.Engine;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Filtering.Default;
using ProjectXyz.Plugins.Features.Filtering.Default.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Skills;
using ProjectXyz.Plugins.Features.Mapping;
using ProjectXyz.Plugins.Features.PartyManagement;
using ProjectXyz.Shared.Framework;

using Xunit;

namespace Macerus.Tests
{
    public sealed class TestAmenities
    {
        private readonly MacerusContainer _container;
        private readonly IMapGameObjectManager _mapGameObjectManager;
        private readonly IGameObjectRepository _gameObjectRepository;
        private readonly IMapStateRepository _mapStateRepository;
        private readonly IMapManager _mapManager;
        private readonly IActorGeneratorFacade _actorGeneratorFacade;
        private readonly IFilterContextAmenity _filterContextAmenity;
        private readonly IActorIdentifiers _actorIdentifiers;
        private readonly IGameObjectIdentifiers _gameObjectIdentifiers;
        private readonly IRosterManager _rosterManager;
        private readonly IGameEngine _gameEngine;
        private readonly IRealTimeManager _realTimeManager;
        private readonly IEncounterManager _encounterManager;

        public TestAmenities(MacerusContainer container)
        {
            _container = container;
            _mapGameObjectManager = _container.Resolve<IMapGameObjectManager>();
            _gameObjectRepository = _container.Resolve<IGameObjectRepository>();
            _mapStateRepository = _container.Resolve<IMapStateRepository>();
            _mapManager = _container.Resolve<IMapManager>();
            _actorGeneratorFacade = _container.Resolve<IActorGeneratorFacade>();
            _filterContextAmenity = _container.Resolve<IFilterContextAmenity>();
            _actorIdentifiers = _container.Resolve<IActorIdentifiers>();
            _gameObjectIdentifiers = _container.Resolve<IGameObjectIdentifiers>();
            _rosterManager = _container.Resolve<IRosterManager>();
            _gameEngine = _container.Resolve<IGameEngine>();
            _realTimeManager = _container.Resolve<IRealTimeManager>();
            _encounterManager = _container.Resolve<IEncounterManager>();
        }

        public IGameObject CreatePlayerInstance()
        {
            var context = _filterContextAmenity.CreateFilterContextForSingle(
                _filterContextAmenity.CreateRequiredAttribute(
                    _gameObjectIdentifiers.FilterContextTypeId,
                    _actorIdentifiers.ActorTypeIdentifier),
                _filterContextAmenity.CreateRequiredAttribute(
                    _actorIdentifiers.ActorDefinitionIdentifier,
                    new StringIdentifier("player")));
            var player = _actorGeneratorFacade
                .GenerateActors(
                    context,
                    new IGeneratorComponent[] { })
                .Single();
            return player;
        }

        public async Task UsingCleanMapAndObjectsWithPlayerAsync(Func<IGameObject, Task> callback)
        {
            await UsingCleanMapAndObjects(async () =>
            {
                var actor = CreatePlayerInstance();

                _mapGameObjectManager.MarkForAddition(actor);
                await _mapGameObjectManager
                    .SynchronizeAsync()
                    .ConfigureAwait(false);
                await callback.Invoke(actor);
            });
        }

        public async Task UsingCleanMapAndObjects(Func<Task> callback)
        {
            ResetGameObjectManagerHack(
                _gameObjectRepository,
                _mapStateRepository);
            await ResetEncounterManagerHackAsync(_encounterManager);

            await _mapManager
                .UnloadMapAsync()
                .ConfigureAwait(false);
            Assert.Empty(_mapGameObjectManager.GameObjects);

            _rosterManager.ClearRoster();
            Assert.Empty(_rosterManager.FullRoster);
            Assert.Empty(_rosterManager.ActiveParty);

            try
            {
                await callback.Invoke();
            }
            finally
            {
                await _mapManager
                    .UnloadMapAsync()
                    .ConfigureAwait(false);
                Assert.Empty(_mapGameObjectManager.GameObjects);

                _rosterManager.ClearRoster();
                Assert.Empty(_rosterManager.FullRoster);
                Assert.Empty(_rosterManager.ActiveParty);
            }

            await ResetEncounterManagerHackAsync(_encounterManager);
            ResetGameObjectManagerHack(
                _gameObjectRepository,
                _mapStateRepository);
        }

        public Task ExecuteBetweenGameEngineUpdatesAsync(TimeSpan elapsed, Func<Task> callback) =>
            ExecuteBetweenGameEngineUpdatesAsync(DateTime.UtcNow, elapsed, callback);

        public async Task ExecuteBetweenGameEngineUpdatesAsync(
            DateTime startTime,
            TimeSpan elapsed,
            Func<Task> callback)
        {
            _realTimeManager.SetTimeUtc(startTime);
            await _gameEngine
                .UpdateAsync()
                .ConfigureAwait(false);

            await callback
                .Invoke()
                .ConfigureAwait(false);

            _realTimeManager.SetTimeUtc(startTime + elapsed);
            await _gameEngine
                .UpdateAsync()
                .ConfigureAwait(false);
        }

        private void ResetGameObjectManagerHack(
            IGameObjectRepository gameObjectRepository,
            IMapStateRepository mapStateRepository)
        {
            var cacheField = typeof(GameObjectRepository)
                .GetField("_cache", BindingFlags.NonPublic | BindingFlags.Instance);
            var cache = (Dictionary<IIdentifier, IGameObject>)cacheField.GetValue(gameObjectRepository);
            cache.Clear();

            if (mapStateRepository is MapStateRepository)
            {
                cacheField = typeof(MapStateRepository)
                    .GetField("_gameObjectIdCache", BindingFlags.NonPublic | BindingFlags.Instance);
                var cache2 = (Dictionary<IIdentifier, List<IIdentifier>>)cacheField.GetValue(mapStateRepository);
                cache2.Clear();
            }
        }

        private async Task ResetEncounterManagerHackAsync(IEncounterManager encounterManager)
        {
            var encounterField = typeof(EncounterManager)
                .GetField("_encounter", BindingFlags.NonPublic | BindingFlags.Instance);

            if (encounterField.GetValue(_encounterManager) != null)
            {
                await _encounterManager
                    .EndEncounterAsync(
                        new FilterContext(new FilterAttribute[0]),
                        Enumerable.Empty<IBehavior>())
                    .ConfigureAwait(false);
            }

            encounterField.SetValue(encounterManager, null);
        }
    }
}
