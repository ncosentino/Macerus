using System.Threading.Tasks;

using Macerus.Plugins.Features.DataPersistence.Kvp;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Game.Api;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Framework;

namespace Macerus.Game.DataPersistence.Kvp
{
    public sealed class GameObjectKvpDataPersistenceHandler : IDiscoverableKvpDataPersistenceWriter
    {
        private readonly IGameObjectRepository _gameObjectRepository;

        public GameObjectKvpDataPersistenceHandler(IGameObjectRepository gameObjectRepository)
        {
            _gameObjectRepository = gameObjectRepository;
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
