using System;

using ProjectXyz.Plugins.Features.Mapping.Api;

namespace Macerus.Tests
{
    public sealed class TestAmenities
    {
        private readonly MacerusContainer _container;
        private readonly IMapGameObjectManager _mapGameObjectManager;

        public TestAmenities(MacerusContainer container)
        {
            _container = container;
            _mapGameObjectManager = _container.Resolve<IMapGameObjectManager>();
        }

        public void UsingCleanObjectManager(Action callback)
        {
            _mapGameObjectManager.MarkForRemoval(_mapGameObjectManager.GameObjects);
            _mapGameObjectManager.Synchronize();
            try
            {
                callback.Invoke();
            }
            finally
            {
                _mapGameObjectManager.MarkForRemoval(_mapGameObjectManager.GameObjects);
                _mapGameObjectManager.Synchronize();
            }
        }
    }
}
