using ProjectXyz.Plugins.Features.Mapping.Api;

namespace Macerus.Plugins.Features.Mapping
{
    public interface ITileResourceComponent : ITileComponent
    {
        string TilesetResourcePath { get; }

        string SpriteResourceName { get; }
    }
}