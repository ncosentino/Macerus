using System;
using System.Collections.Generic;
using System.Reflection;

using Macerus.Game;
using Macerus.Plugins.Features.Mapping.TiledNet;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Game.Api;
using ProjectXyz.Plugins.Features.Mapping.Api;

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

        public TestAmenities(MacerusContainer container)
        {
            _container = container;
            _mapGameObjectManager = _container.Resolve<IMapGameObjectManager>();
            _gameObjectRepository = _container.Resolve<IGameObjectRepository>();
            _mapGameObjectRepository = _container.Resolve<IMapGameObjectRepository>();
            _mapManager = _container.Resolve<IMapManager>();
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

            cacheField = typeof(TiledNetGameObjectRepository)
                .GetField("_gameObjectIdCache", BindingFlags.NonPublic | BindingFlags.Instance);
            var cache2 = (Dictionary<IIdentifier, List<IIdentifier>>)cacheField.GetValue(mapGameObjectRepository);
            cache2.Clear();
        }
    }
}
