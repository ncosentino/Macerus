using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Macerus.Api.Behaviors.Filtering;
using Macerus.Api.GameObjects;
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
        private readonly IMapGameObjectRepository _mapGameObjectRepository;
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
            _mapGameObjectRepository = _container.Resolve<IMapGameObjectRepository>();
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

        public void UsingCleanMapAndObjectsWithPlayer(Action<IGameObject> callback)
        {
            var actor = CreatePlayerInstance();
            UsingCleanMapAndObjects(() =>
            {
                _mapGameObjectManager.MarkForAddition(actor);
                _mapGameObjectManager.Synchronize();

                callback.Invoke(actor);
            });
        }

        public void UsingCleanMapAndObjects(Action callback)
        {
            ResetGameObjectManagerHack(
                _gameObjectRepository,
                _mapGameObjectRepository);

            _mapManager.UnloadMap();
            Assert.Empty(_mapGameObjectManager.GameObjects);

            try
            {
                callback.Invoke();
            }
            finally
            {
                _mapManager.UnloadMap();
                Assert.Empty(_mapGameObjectManager.GameObjects);
            }
        }

        private void ResetGameObjectManagerHack(
            IGameObjectRepository gameObjectRepository,
            IMapGameObjectRepository mapGameObjectRepository)
        {
            var cacheField = typeof(GameObjectRepository)
                .GetField("_cache", BindingFlags.NonPublic | BindingFlags.Instance);
            var cache = (Dictionary<IIdentifier, IGameObject>)cacheField.GetValue(gameObjectRepository);
            cache.Clear();

            if (mapGameObjectRepository is MapGameObjectRepository)
            {
                cacheField = typeof(MapGameObjectRepository)
                    .GetField("_gameObjectIdCache", BindingFlags.NonPublic | BindingFlags.Instance);
                var cache2 = (Dictionary<IIdentifier, List<IIdentifier>>)cacheField.GetValue(mapGameObjectRepository);
                cache2.Clear();
            }
        }
    }
}
