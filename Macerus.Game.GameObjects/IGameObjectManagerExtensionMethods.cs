using System.Linq;

using Macerus.Api.Behaviors;

using ProjectXyz.Api.GameObjects;

namespace Macerus.Game.GameObjects
{
    public static class IGameObjectManagerExtensionMethods
    {
        public static IGameObject GetPlayer(this IGameObjectManager gameObjectManager)
        {
            var player = gameObjectManager
                .GameObjects
                .FirstOrDefault(x => x.Has<IPlayerControlledBehavior>());
            return player;
        }
    }
}
