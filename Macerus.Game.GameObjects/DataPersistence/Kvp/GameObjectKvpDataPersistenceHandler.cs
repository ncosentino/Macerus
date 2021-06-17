using System;
using System.Linq;
using System.Threading.Tasks;

using Macerus.Plugins.Features.DataPersistence.Kvp;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Game.Api;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
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

                   return new
                   {
                       Key = x,
                       ObjectId = new StringIdentifier(strId.Identifier.Substring("ObjectState/".Length)),
                   };
               })
               .Where(x => x != null);
            foreach (var objectStateKey in objectStateKeys)
            {
                var obj = (IGameObject)await reader.ReadAsync(objectStateKey.ObjectId);
                _gameObjectRepository.Save(obj);
            }
        }

        public async Task WriteAsync(IKvpDataStoreWriter writer)
        {
            foreach (var obj in _gameObjectRepository.LoadAll())
            {
                await writer.WriteAsync(
                    new StringIdentifier($"ObjectState/{obj.GetOnly<IReadOnlyIdentifierBehavior>().Id}"),
                    obj);
            }
        }
    }
}
