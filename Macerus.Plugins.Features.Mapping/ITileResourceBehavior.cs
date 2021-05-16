using ProjectXyz.Api.GameObjects.Behaviors;

namespace Macerus.Plugins.Features.Mapping
{
    public interface ITileResourceBehavior : IBehavior
    {
        string TilesetResourcePath { get; }

        string SpriteResourceName { get; }
    }
}