using System.Linq;

using Macerus.Api.Behaviors;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Mapping;

namespace Macerus.Game
{
    // FIXME: based on the patterns we're using, this should be an 'Amenity' not extension method?
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
