using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

using Tiled.Net.Maps;

namespace Macerus.Plugins.Features.Mapping.TiledNet
{
    public interface ITiledNetToMapConverter
    {
        IGameObject Convert(
            IIdentifier mapId,
            ITiledMap tiledMap);
    }
}