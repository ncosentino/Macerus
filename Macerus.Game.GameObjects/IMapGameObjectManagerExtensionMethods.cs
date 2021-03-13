using System.Linq;

using Macerus.Api.Behaviors;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Mapping.Api;

namespace Macerus.Game.GameObjects
{
    public static class IMapGameObjectManagerExtensionMethods
    {
        public static IGameObject GetPlayer(this IReadOnlyMapGameObjectManager mapGameObjectManager)
        {
            var player = mapGameObjectManager
                .GameObjects
                .FirstOrDefault(x => x.Has<IPlayerControlledBehavior>());
            return player;
        }
    }
}
