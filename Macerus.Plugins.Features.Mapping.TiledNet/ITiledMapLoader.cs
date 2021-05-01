using ProjectXyz.Api.Framework;

using Tiled.Net.Maps;

namespace Macerus.Plugins.Features.Mapping.TiledNet
{
    public interface ITiledMapLoader
    {
        ITiledMap LoadMap(IIdentifier mapId);
    }
}