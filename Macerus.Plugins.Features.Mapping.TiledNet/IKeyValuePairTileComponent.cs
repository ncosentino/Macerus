using ProjectXyz.Plugins.Features.Mapping.Api;

namespace Macerus.Plugins.Features.Mapping.TiledNet
{
    public interface IKeyValuePairTileComponent : ITileComponent
    {
        string Key { get; }

        object Value { get; }
    }
}