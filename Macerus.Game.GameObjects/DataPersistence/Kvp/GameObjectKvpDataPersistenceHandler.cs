using System;
using System.Linq;
using System.Threading.Tasks;

using Macerus.Plugins.Features.DataPersistence.Kvp;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Game.Api;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.CommonBehaviors.Filtering;
using ProjectXyz.Shared.Framework;

namespace Macerus.Game.DataPersistence.Kvp
{
    public sealed class GameObjectKvpDataPersistenceHandler :
        IDiscoverableKvpDataPersistenceWriter,
        IDiscoverableKvpDataPersistenceReader
    {
        private readonly IGameObjectRepository _gameObjectRepository;

        public GameObjectKvpDataPersistenceHandler(IGameObjectRepository gameObjectRepository)
        {
            _gameObjectRepository = gameObjectRepository;
        }

        public async Task ReadAsync(IKvpDataStoreReader reader)
        {
            _gameObjectRepository.Clear();
            var objectStateKeys = reader
               .GetKeys()
               .Select(x =>
               {
                   if (!(x is StringIdentifier strId))
                   {
                       return null;
                   }

                   if (!strId.Identifier.StartsWith("ObjectState/", StringComparison.Ordinal))
                   {
                       return null;
                   }

                   return x;
               })
               .Where(x => x != null);
            foreach (var objectStateKey in objectStateKeys)
            {
                var obj = await reader.ReadAsync<IGameObject>(objectStateKey);
                _gameObjectRepository.Save(obj);
            }
        }

        public async Task WriteAsync(IKvpDataStoreWriter writer)
        {
            foreach (var obj in _gameObjectRepository.Load(new[] { new PredicateFilter(_ => true) }))
            {
                await writer.WriteAsync(
                    new StringIdentifier($"ObjectState/{obj.GetOnly<IReadOnlyIdentifierBehavior>().Id}"),
                    obj);
            }
        }
    }
}
