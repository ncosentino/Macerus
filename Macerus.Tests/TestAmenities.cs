using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using Macerus.Api.Behaviors.Filtering;
using Macerus.Game;
using Macerus.Plugins.Features.GameObjects.Actors.Generation;
using Macerus.Plugins.Features.Mapping.Default;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Game.Api;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Plugins.Features.Mapping.Api;
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

        public async Task UsingCleanMapAndObjectsWithPlayer(Func<IGameObject, Task> callback)
        {
            var actor = CreatePlayerInstance();
            await UsingCleanMapAndObjects(async () =>
            {
                _mapGameObjectManager.MarkForAddition(actor);
                _mapGameObjectManager.Synchronize();

                await callback.Invoke(actor);
            });
        }

        public async Task UsingCleanMapAndObjects(Func<Task> callback)
        {
            ResetGameObjectManagerHack(
                _gameObjectRepository,
                _mapStateRepository);

            _mapManager.UnloadMap();
            Assert.Empty(_mapGameObjectManager.GameObjects);

            try
            {
                await callback.Invoke();
            }
            finally
            {
                _mapManager.UnloadMap();
                Assert.Empty(_mapGameObjectManager.GameObjects);
            }
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
    }
}
