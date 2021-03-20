using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Mapping.Api;

using Tiled.Net.Maps;

namespace Macerus.Plugins.Features.Mapping.TiledNet
{
    public interface ITiledNetToMapConverter
    {
        IMap Convert(
            IIdentifier mapId,
            ITiledMap tiledMap);
    }
}